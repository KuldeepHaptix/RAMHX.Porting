﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="RAMHX.CMS.Web.FileBrowser.FileBrowser"
    CodeBehind="FileBrowser.aspx.cs" %>

<%@ Register Assembly="IZ.WebFileManager" Namespace="IZ.WebFileManager" TagPrefix="iz" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>File Browser</title>
    <link href="/FileBrowser/css/fineuploader.css" rel="stylesheet" />
    <link href="/FileBrowser/css/style.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="HF_CurrentCulture" runat="server" />
        <asp:HiddenField ID="HF_CKEditorFunctionNumber" runat="server" />
        <asp:HiddenField ID="HF_Opener" runat="server" />
        <asp:HiddenField ID="HF_CallBack" runat="server" />
        <asp:HiddenField ID="HF_Field" runat="server" />
        <asp:HiddenField ID="HF_EditableFiles" runat="server" />
        <input id="HF_FileBrowserConfig" type="hidden" runat="server"
            data-imagefolder="image"
            data-flashfolder="flash"
            data-filesfolder="files"
            data-mediafolder="media"
            data-readonly-hidecommands="false" />
        <input id="HF_CustomRoots" type="hidden" runat="server"
            data-usecustomroots="false"
            data-usedefaultroots="true"
            data-roots-names="Pippo,Pluto,Paperino"
            data-roots-smallimages="file-video.png,file-word.png,file-zip-alt.png"
            data-roots-largeimages="file-video.png,file-word.png,file-zip-alt.png"
            data-roots-folders=""
            data-roots-imagefolder="/FileBrowser/img/32" />
        <div>
            <iz:FileManager ID="FileManager1" runat="server" Height="480" Width="570" ImagesFolder="~/FileBrowser/img/cmd"
                MainDirectory="~/" CustomThumbnailHandler="~/FileBrowser/IZWebFileManagerThumbnailHandler.ashx"
                ShowHiddenFilesAndFolders="false" FileViewMode="Thumbnails" ClientOpenItemFunction="fileSelected"
                DefaultAccessMode="Write"
                ShowAddressBar="true" ShowSearchBox="true" AllowDelete="true" AllowOverwrite="true" AllowUpload="true">
                <RootDirectories>
                    <iz:RootDirectory DirectoryPath="~/" Text="Root" />
                </RootDirectories>
                <CustomToolbarButtons>
                    <iz:CustomToolbarButton ImageUrl="img/cmd/Show.png" Text="Mostra file" PerformPostBack="false"
                        OnClientClick="showFile()" />
                    <iz:CustomToolbarButton ImageUrl="img/cmd/editfile.png" Text="Edit" PerformPostBack="false"
                        OnClientClick="editFile()" />
                    <iz:CustomToolbarButton ImageUrl="img/cmd/clipboard.png" Text="Copy Path" PerformPostBack="false"
                        OnClientClick="copyPath()" />
                    <iz:CustomToolbarButton ImageUrl="img/cmd/file-zip-alt.png" Text="Extract here" PerformPostBack="false"
                        OnClientClick="extractHere()" />
                </CustomToolbarButtons>
                <FileTypes>
                    <iz:FileType Extensions=".jpg, .jpeg,.pmg, .gif" SmallImageUrl="img/16/file-picture.png"
                        LargeImageUrl="img/32/file-picture.png">
                    </iz:FileType>
                    <iz:FileType Extensions=".xls,.xlsx,.ods" SmallImageUrl="img/16/file-excel-alt.png"
                        LargeImageUrl="img/32/file-excel-alt.png">
                    </iz:FileType>
                    <iz:FileType Extensions=".pdf" SmallImageUrl="img/16/file-pdf-alt.png" LargeImageUrl="img/32/file-pdf-alt.png">
                    </iz:FileType>
                    <iz:FileType Extensions=".psd" SmallImageUrl="img/16/file-photoshop.png" LargeImageUrl="img/32/file-photoshop.png">
                    </iz:FileType>
                    <iz:FileType Extensions=".ppt,.pptx,.pptm, .odp" SmallImageUrl="img/16/file-powerpoint-alt.png"
                        LargeImageUrl="img/32/file-powerpoint-alt.png">
                    </iz:FileType>
                    <iz:FileType Extensions=".aiff,.mp3,.ogg,.oga,.wav,.wma" SmallImageUrl="img/16/file-sound.png"
                        LargeImageUrl="img/32/file-sound.png">
                    </iz:FileType>
                    <iz:FileType Extensions=".txt" SmallImageUrl="img/16/file-text.png" LargeImageUrl="img/32/file-text.png">
                    </iz:FileType>
                    <iz:FileType Extensions=".flv,.f4v,.avi,.mov,.qt,.wmv,.mp4,.mpg,.mpeg,.mp2" SmallImageUrl="img/16/file-video.png"
                        LargeImageUrl="img/32/file-video.png">
                    </iz:FileType>
                    <iz:FileType Extensions=".doc,.docx,.odt" SmallImageUrl="img/16/file-word-alt.png"
                        LargeImageUrl="img/32/file-word.png">
                    </iz:FileType>
                    <iz:FileType Extensions=".tar.gz, .7z, .ace, .cab, .rar, .zip, .zipx" SmallImageUrl="img/16/file-zip-alt.png"
                        LargeImageUrl="img/32/file-zip-alt.png">
                    </iz:FileType>
                </FileTypes>
            </iz:FileManager>
            <asp:Panel ID="Panel_upload" runat="server" CssClass="upload-group">
                <a class="btn btn-xs btn-default" data-action="upload" id="Upload_button" runat="server">Click to upload</a>
                <small id="DND_message" runat="server"></small>
            </asp:Panel>
            <asp:Panel ID="Panel_deny" runat="server" CssClass="alert alert-danger" Visible="false">
            </asp:Panel>
        </div>
        <div class="modal fade " id="modalShowFile">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span
                                class="sr-only">Close</span></button>
                        <h4 class="modal-title">Modal title</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <asp:PlaceHolder runat="server" ID="plcEditFile">
            <div class="modal fade " id="modelpageedit">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span
                                    class="sr-only">Close</span></button>
                            <h4 class="modal-title" id="editorTitle"></h4>
                        </div>
                        <div class="modal-body">
                            <iframe id="ifrmEditFile" height="480" width="100%" style="border: 0px;"></iframe>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </asp:PlaceHolder>

        <!-- /.modal -->
    </form>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script type="text/javascript">
        if (typeof jQuery == 'undefined') {
            document.write(unescape("%3Cscript src='/Scripts/jquery-1.11.1.min.js' type='text/javascript'%3E%3C/script%3E"));
        }
    </script>
    <script src="/FileBrowser/js/FileManager.min.js"></script>
    <script>
        var uploader;
        var $filemanager, $fileview, $foldertree;

        var fileSelected = function (fileUrl) {
            var win;
            var opener = $('#HF_Opener').val();
            var fn = $('#HF_CallBack').val();
            fileUrl = fileUrl.replace('//', '/');
            switch (opener) {
                case 'ckeditor':
                    win = window.opener;
                    if (win.CKEDITOR)
                        win.CKEDITOR.tools.callFunction($('#HF_CKEditorFunctionNumber').val(), fileUrl);
                    break;
                case 'opener':
                    win = window.opener;
                    win[fn](fileUrl);
                    break;
                case 'parent':
                    win = window.parent;
                    win[fn](fileUrl);
                    break;
                case 'top':
                    win = window.top;
                    win[fn](fileUrl);
                    break;
                case 'tinymce4':
                    var params = window.parent.tinyMCE.activeEditor.windowManager.getParams();
                    var theField = window.parent.document.getElementById(params.field);
                    if (theField) {
                        theField.value = fileUrl;
                        window.parent.tinyMCE.activeEditor.windowManager.close();
                        return true;
                    }
                    break;
                case 'internal':
                    //PageLayoutPath
                    break;

                default:
                    alert(fileUrl);

            }

            if (window.opener)
                window.close();
        }

        /**
            Rising and handling CallBack protocol to implement custom toolbar button.
            Hera is used to display images in a bootstrap modal, but it's a sample 
            how to handle custom client command with  WebFileManager
        */
        var showFile = function () {
            var args;
            args = [];
            args.push('showfile');
            // Get name of selected file and call server to get complete url
            if (WFM_FileManager1_FileView.SelectedItems.length) {
                args.push(WFM_FileManager1_FileView.SelectedItems[0].Path.replace('//', '/'));
                CallServer(args.join(','), "");
            }
        }

        var editFile = function () {
            var args;
            args = [];
            args.push('editfile');
            // Get name of selected file and call server to get complete url
            if (WFM_FileManager1_FileView.SelectedItems.length) {
                args.push(WFM_FileManager1_FileView.SelectedItems[0].Path.replace('//', '/'));
                CallServer(args.join(','), "");
            }
        }

        var copyPath = function () {
            var args;
            args = [];
            args.push('copypath');
            //   Get name of selected file and call server to get complete url
            if (WFM_FileManager1_FileView.SelectedItems.length) {
                args.push(WFM_FileManager1_FileView.SelectedItems[0].Path.replace('//', '/'));
                CallServer(args.join(','), "");
            }
        }

        var extractHere = function () {
            var args;
            args = [];
            args.push('extracthere');

            //   Get name of selected file and call server to get complete url
            if (WFM_FileManager1_FileView.SelectedItems.length) {
                args.push(WFM_FileManager1_FileView.SelectedItems[0].Path.replace('//', '/'));
                CallServer(args.join(','), "");
            }
        }

        // Handle of CallBack response
        function ReceiveServerData(rValue) {
            var obj = $.parseJSON(rValue);
            switch (obj.command) {
                case 'showfile':
                    var $dialog = $('#modalShowFile');
                    var fName = obj.data.replace('//', '/');
                    var extension = fName.substr(fName.lastIndexOf("."));
                    var isImage = (['.jpg', '.jpeg', '.png', '.gif']).indexOf(extension) != -1;
                    if (isImage) {
                        $dialog
                            .find('.modal-body')
                            .html('')
                            .append($('<img />').attr('src', fName).addClass('img-responsive'));
                        $dialog.find('.modal-title').text(fName);
                        $dialog.modal();
                    } else {
                        window.open(fName, '_blank', 'height=480, width=640, location=no, menubar=no, toolbar=no, left=50, top= 50');
                    }
                    break;
                case 'editfile':
                    var $dialog = $('#modelpageedit');
                    var fName = obj.data.replace('//', '/');
                    var extension = fName.substr(fName.lastIndexOf("."));
                    //var isEditable = (['.cshtml', '.txt', '.css', '.js', '.html', '.config', '.xml', '.json', '.htm']).indexOf(extension) != -1;
                    var editableFiles = $('[id*=HF_EditableFiles').val();
                    var isEditable = (editableFiles.split(',')).indexOf(extension) != -1;
                    if (isEditable) {
                        $dialog.find('.modal-title').text('Edit - ' + fName);
                        $dialog.find('#ifrmEditFile').attr('src', '/Admin/FileManager/FileEdit?filepath=' + fName);
                        $dialog.modal();
                    } else {
                        alert("File type is not supported to edit");
                    }
                    break;
                case 'upload':
                    var uploads = uploader.getUploads({
                        status: qq.status.SUBMITTED
                    });
                    for (var file in uploads) {
                        uploader.setParams({
                            culture: $('#HF_CurrentCulture').val(),
                            dest: obj.data
                        });

                    }
                    uploader.uploadStoredFiles();
                    break;

                case 'copypath':
                    var fName = obj.data.replace('//', '/');
                    window.prompt("Copy to clipboard: Ctrl+C, Enter", fName);
                    break;

                case 'extracthere':
                    alert(obj.data);
                    var url = $('#FileManager1_FileView_Address').val();
                    WFM_FileManager1_FileView.Navigate(url);
                    break;

                default:

            }
        }

        $(function () {
            $("body").mb_waitspinner({
                locked: true,
                windowColor: "rgba(21,21,21,0.8)",
                color: "#FFF"
            })
        });

        // Hiding dran'n drop message if drag 'n drop is'nt supported
        $(function () {
            var iOS = !!navigator.userAgent.match('iPhone OS') || !!navigator.userAgent.match('iPad');
            var div = document.createElement('div');
            var dnd = ('draggable' in div) || ('ondragstart' in div && 'ondrop' in div);
            if (iOS || !dnd)
                $('#DND_message').html('');
        });


        // Resizing File Manager
        $(function () {
            $filemanager = $('#Filemanager1');
            $fileview = $('#FileManager1_FileView');
            $foldertree = $('#FileManager1_FolderTree');
            $foldertree.width(150);
            $(window).on('resize load', function (e) {
                var $this = $(this);
                var w = $this.width();
                var h = $this.height();
                var $toolbar = $('#FileManager1_ToolBar');
                var $paneUpload = $('#Panel_upload');
                var barsHeight = ($toolbar.length ? $toolbar.height() + 5 : 5) + ($paneUpload.length ? $paneUpload.height() + 5 : 5);
                $filemanager.width(w - 15);
                $fileview.width(w - $foldertree.width() - 15).height(h - barsHeight);
                $foldertree.height(h - barsHeight);
            })
        })

        $(function () {
            /////////////////////////////////////////////////////////////////////
            ///// Upload ---------------------------------------------------////
            /////////////////////////////////////////////////////////////////////

            var uploadBtn = $('[data-action="upload"]')[0];

            uploader = new qq.FineUploaderBasic({
                button: uploadBtn,
                autoUpload: false,
                multiple: true,
                request: {
                    endpoint: 'MyUpload.ashx',
                    params: {
                        culture: $('#HF_CurrentCulture').val()
                    }
                },
                callbacks: {
                    onSubmitted: function (id, fileName, loaded, total) {
                        CallServer('upload', '');
                    },
                    onUpload: function (id, fileName) {
                        $fileview.spin();
                    },
                    onComplete: function (id, fileName, responseJSON) {
                        if (responseJSON.success) {
                            WFM_FileManager1_Controller.OnRefresh(WFM_FileManager1_FileView, '');
                        } else {
                            bootbox.alert('Si è verificato un errore: ' + responseJSON.msg);
                        }
                        $fileview.spin(false);
                    }
                }
            });

            $fileview.fineUploaderDnd({
                allowMultipleItems: true
            }).on('processingDroppedFilesComplete', function (event, files, dropTarget) {

                uploader.addFiles(files);
            });
            /////////////////////////////////////////////////////////////////////
            ///// Fine Upload ----------------------------------------------////
            /////////////////////////////////////////////////////////////////////
        });
    </script>
</body>
</html>
