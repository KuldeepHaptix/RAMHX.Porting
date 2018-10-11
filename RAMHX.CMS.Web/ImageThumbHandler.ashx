<%@ WebHandler Language="C#" Class="ImageThumbHandler" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public class ImageThumbHandler : System.Web.IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        String file = context.Request.QueryString["f"];
        String size = context.Request.QueryString["s"];

        if (!string.IsNullOrEmpty(file) && File.Exists(context.Server.MapPath(file)))
        {
            context.Response.Clear();
            //context.Response.ContentType = "application/octet-stream";
            context.Response.ContentType = "image/png";
            //context.Response.AddHeader("content-disposition", "inline;filename=" + Path.GetFileName(file));
            int THUMB_SIZE = 150;
            if (!string.IsNullOrEmpty(size))
            {
                int.TryParse(size, out THUMB_SIZE);
                if (THUMB_SIZE == 0)
                {
                    THUMB_SIZE = 150;
                }
            }
            Bitmap thumbnail = WindowsThumbnailProvider.GetThumbnail(
               context.Server.MapPath(file), THUMB_SIZE, THUMB_SIZE, ThumbnailOptions.ThumbnailOnly);

            context.Response.BinaryWrite(ImageToByte(thumbnail));
            // This would be the ideal spot to collect some download statistics and / or tracking  
            // also, you could implement other requests, such as delete the file after download  
            context.Response.End();

        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("File not be found!");


        }
    }
    public static byte[] ImageToByte(Image img)
    {
        ImageConverter converter = new ImageConverter();
        return (byte[])converter.ConvertTo(img, typeof(byte[]));
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public void GenerateThumbNail(string sourcefile, string destinationfile, int width)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(sourcefile));
        int srcWidth = image.Width;
        int srcHeight = image.Height;
        int thumbWidth = width;
        int thumbHeight;
        Bitmap bmp;
        if (srcHeight > srcWidth)
        {
            thumbHeight = (srcHeight / srcWidth) * thumbWidth;
            bmp = new Bitmap(thumbWidth, thumbHeight);
        }
        else
        {
            thumbHeight = thumbWidth;
            thumbWidth = (srcWidth / srcHeight) * thumbHeight;
            bmp = new Bitmap(thumbWidth, thumbHeight);
        }

        System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
        gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        System.Drawing.Rectangle rectDestination =
               new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
        gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
        bmp.Save(System.Web.HttpContext.Current.Server.MapPath(destinationfile));
        bmp.Dispose();
        image.Dispose();
    }

}


[Flags]
public enum ThumbnailOptions
{
    None = 0x00,
    BiggerSizeOk = 0x01,
    InMemoryOnly = 0x02,
    IconOnly = 0x04,
    ThumbnailOnly = 0x08,
    InCacheOnly = 0x10,
}

public class WindowsThumbnailProvider
{
    private const string IShellItem2Guid = "7E9FB0D3-919F-4307-AB2E-9B1860310C93";

    [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int SHCreateItemFromParsingName(
        [MarshalAs(UnmanagedType.LPWStr)] string path,
        // The following parameter is not used - binding context.
        IntPtr pbc,
        ref Guid riid,
        [MarshalAs(UnmanagedType.Interface)] out IShellItem shellItem);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool DeleteObject(IntPtr hObject);

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
    internal interface IShellItem
    {
        void BindToHandler(IntPtr pbc,
            [MarshalAs(UnmanagedType.LPStruct)]Guid bhid,
            [MarshalAs(UnmanagedType.LPStruct)]Guid riid,
            out IntPtr ppv);

        void GetParent(out IShellItem ppsi);
        void GetDisplayName(SIGDN sigdnName, out IntPtr ppszName);
        void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);
        void Compare(IShellItem psi, uint hint, out int piOrder);
    };

    internal enum SIGDN : uint
    {
        NORMALDISPLAY = 0,
        PARENTRELATIVEPARSING = 0x80018001,
        PARENTRELATIVEFORADDRESSBAR = 0x8001c001,
        DESKTOPABSOLUTEPARSING = 0x80028000,
        PARENTRELATIVEEDITING = 0x80031001,
        DESKTOPABSOLUTEEDITING = 0x8004c000,
        FILESYSPATH = 0x80058000,
        URL = 0x80068000
    }

    internal enum HResult
    {
        Ok = 0x0000,
        False = 0x0001,
        InvalidArguments = unchecked((int)0x80070057),
        OutOfMemory = unchecked((int)0x8007000E),
        NoInterface = unchecked((int)0x80004002),
        Fail = unchecked((int)0x80004005),
        ElementNotFound = unchecked((int)0x80070490),
        TypeElementNotFound = unchecked((int)0x8002802B),
        NoObject = unchecked((int)0x800401E5),
        Win32ErrorCanceled = 1223,
        Canceled = unchecked((int)0x800704C7),
        ResourceInUse = unchecked((int)0x800700AA),
        AccessDenied = unchecked((int)0x80030005)
    }

    [ComImportAttribute()]
    [GuidAttribute("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItemImageFactory
    {
        [PreserveSig]
        HResult GetImage(
        [In, MarshalAs(UnmanagedType.Struct)] NativeSize size,
        [In] ThumbnailOptions flags,
        [Out] out IntPtr phbm);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeSize
    {
        private int width;
        private int height;

        public int Width { set { width = value; } }
        public int Height { set { height = value; } }
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBQUAD
    {
        public byte rgbBlue;
        public byte rgbGreen;
        public byte rgbRed;
        public byte rgbReserved;
    }

    public static Bitmap GetThumbnail(string fileName, int width, int height, ThumbnailOptions options)
    {
        IntPtr hBitmap = GetHBitmap(Path.GetFullPath(fileName), width, height, options);

        try
        {
            // return a System.Drawing.Bitmap from the hBitmap
            return GetBitmapFromHBitmap(hBitmap);
        }
        finally
        {
            // delete HBitmap to avoid memory leaks
            DeleteObject(hBitmap);
        }
    }

    public static Bitmap GetBitmapFromHBitmap(IntPtr nativeHBitmap)
    {
        Bitmap bmp = Bitmap.FromHbitmap(nativeHBitmap);

        if (Bitmap.GetPixelFormatSize(bmp.PixelFormat) < 32)
            return bmp;

        return CreateAlphaBitmap(bmp, PixelFormat.Format32bppArgb);
    }

    public static Bitmap CreateAlphaBitmap(Bitmap srcBitmap, PixelFormat targetPixelFormat)
    {
        Bitmap result = new Bitmap(srcBitmap.Width, srcBitmap.Height, targetPixelFormat);

        Rectangle bmpBounds = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);

        BitmapData srcData = srcBitmap.LockBits(bmpBounds, ImageLockMode.ReadOnly, srcBitmap.PixelFormat);

        bool isAlplaBitmap = false;

        try
        {
            for (int y = 0; y <= srcData.Height - 1; y++)
            {
                for (int x = 0; x <= srcData.Width - 1; x++)
                {
                    Color pixelColor = Color.FromArgb(
                        Marshal.ReadInt32(srcData.Scan0, (srcData.Stride * y) + (4 * x)));

                    if (pixelColor.A > 0 & pixelColor.A < 255)
                    {
                        isAlplaBitmap = true;
                    }

                    result.SetPixel(x, y, pixelColor);
                }
            }
        }
        finally
        {
            srcBitmap.UnlockBits(srcData);
        }

        if (isAlplaBitmap)
        {
            return result;
        }
        else
        {
            return srcBitmap;
        }
    }

    private static IntPtr GetHBitmap(string fileName, int width, int height, ThumbnailOptions options)
    {
        IShellItem nativeShellItem;
        Guid shellItem2Guid = new Guid(IShellItem2Guid);
        int retCode = SHCreateItemFromParsingName(fileName, IntPtr.Zero, ref shellItem2Guid, out nativeShellItem);

        if (retCode != 0)
            throw Marshal.GetExceptionForHR(retCode);

        NativeSize nativeSize = new NativeSize();
        nativeSize.Width = width;
        nativeSize.Height = height;

        IntPtr hBitmap;
        HResult hr = ((IShellItemImageFactory)nativeShellItem).GetImage(nativeSize, options, out hBitmap);

        Marshal.ReleaseComObject(nativeShellItem);

        if (hr == HResult.Ok) return hBitmap;

        throw Marshal.GetExceptionForHR((int)hr);
    }
}
