/*!
 * Yjsgroundprogress      1.0.0
 * @copyright    Copyright(C) since 2007  Youjoomla.com. All Rights Reserved.
 * @author       YouJoomla
 * @license      http://www.gnu.org/licenses/gpl-2.0.html GNU/GPLv2 only
 * @websites     http://www.youjoomla.com | http://www.yjsimplegrid.com
 */
! function(t) {
    t.fn.yjsgroundprogress = function(e) {
        var n = t.extend({
            starting: this.data("start"),
            ending: this.data("end"),
            percentage: !0,
            color: this.css("border-top-color"),
            lineWidth: this.data("border"),
            timer: this.data("speed"),
            fontStyle: this.css("font-family"),
            fontSize: this.css("font-size"),
            fontColor: this.css("color"),
            backgroundColor: this.css("border-bottom-color"),
            callback: function() {}
        }, e);
        n.starting || (n.starting = 0), n.ending || (n.ending = 100), void 0 !== this.data("percent") && (n.percentage = this.data("percent")), n.timer || (n.timer = 10), n.lineWidth || (n.lineWidth = 5);
        var i = Math.random();
        this.empty().append("<canvas height =" + this.height() + " width=" + this.width() + " id='my-canvas-" + i + "'/ ></canvas>");
        var r = document.getElementById("my-canvas-" + i),
            a = r.width / 2,
            o = r.height / 2,
            s = this.width() / 2 - n.lineWidth,
            c = r.getContext("2d");
        if (n.backgroundColor) {
            var l = r.getContext("2d");
            l.arc(a, o, s, 0, 2 * Math.PI, !1), l.strokeStyle = n.backgroundColor, l.lineWidth = n.lineWidth, l.stroke()
        }
        var d = n.ending - n.starting,
            h = n.starting,
            f = setInterval(function() {
                var t;
                t = n.percentage ? h + "%" : h;
                var e = (1.5 + h / 50) * Math.PI,
                    i = (1.5 + ++h / 50) * Math.PI;
                c.beginPath(), c.arc(a, o, s, e, i, !1), c.lineWidth = n.lineWidth, c.strokeStyle = n.color, c.stroke(), c.font = n.fontSize + " " + n.fontStyle, c.textAlign = "center", c.textBaseline = "middle", c.fillStyle = n.fontColor, c.clearRect(a - 1.5 * parseInt(n.fontSize), o - parseInt(n.fontSize) / 2, 3 * parseInt(n.fontSize), parseInt(n.fontSize)), c.fillText(t, a, o), h >= d && (window.clearInterval(f), t = n.percentage ? h + "%" : h, c.clearRect(a - 1.5 * parseInt(n.fontSize), o - parseInt(n.fontSize) / 2, 3 * parseInt(n.fontSize), parseInt(n.fontSize)), c.fillText(t, a, o), "function" == typeof n.callback && n.callback.call(this))
            }, n.timer)
    }
}(jQuery), jQuery(function() {
    jQuery("[data-yjsg-round-progress]").each(function() {
        jQuery(this).yjsgroundprogress()
    })
});