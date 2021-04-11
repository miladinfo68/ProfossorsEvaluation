﻿/*
 Highcharts JS v8.0.0 (2019-12-10)

 Highcharts funnel module

 (c) 2010-2019 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (b) { "object" === typeof module && module.exports ? (b["default"] = b, module.exports = b) : "function" === typeof define && define.amd ? define("highcharts/modules/funnel", ["highcharts"], function (c) { b(c); b.Highcharts = c; return b }) : b("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (b) {
    function c(b, w, c, q) { b.hasOwnProperty(w) || (b[w] = q.apply(null, c)) } b = b ? b._modules : {}; c(b, "modules/funnel.src.js", [b["parts/Globals.js"], b["parts/Utilities.js"]], function (b, c) {
        var w = c.pick; c = b.seriesType; var q = b.seriesTypes,
            J = b.fireEvent, E = b.addEvent, G = b.noop; c("funnel", "pie", { animation: !1, center: ["50%", "50%"], width: "90%", neckWidth: "30%", height: "100%", neckHeight: "25%", reversed: !1, size: !0, dataLabels: { connectorWidth: 1, verticalAlign: "middle" }, states: { select: { color: "#cccccc", borderColor: "#000000" } } }, {
                animate: G, translate: function () {
                    function a(b, a) { return /%$/.test(b) ? a * parseInt(b, 10) / 100 : parseInt(b, 10) } var b = 0, e = this, k = e.chart, f = e.options, h = f.reversed, d = f.ignoreHiddenPoint, c = k.plotWidth; k = k.plotHeight; var r = 0, I = f.center, g =
                        a(I[0], c), l = a(I[1], k), q = a(f.width, c), t, u = a(f.height, k), y = a(f.neckWidth, c), F = a(f.neckHeight, k), z = l - u / 2 + u - F; c = e.data; var B, C, E = "left" === f.dataLabels.position ? 1 : 0, A, m, D, v, n, x, p; e.getWidthAt = function (b) { var a = l - u / 2; return b > z || u === F ? y : y + (q - y) * (1 - (b - a) / (u - F)) }; e.getX = function (b, a, d) { return g + (a ? -1 : 1) * (e.getWidthAt(h ? 2 * l - b : b) / 2 + d.labelDistance) }; e.center = [g, l, u]; e.centerX = g; c.forEach(function (a) { d && !1 === a.visible || (b += a.y) }); c.forEach(function (a) {
                            p = null; C = b ? a.y / b : 0; m = l - u / 2 + r * u; n = m + C * u; t = e.getWidthAt(m);
                            A = g - t / 2; D = A + t; t = e.getWidthAt(n); v = g - t / 2; x = v + t; m > z ? (A = v = g - y / 2, D = x = g + y / 2) : n > z && (p = n, t = e.getWidthAt(z), v = g - t / 2, x = v + t, n = z); h && (m = 2 * l - m, n = 2 * l - n, null !== p && (p = 2 * l - p)); B = ["M", A, m, "L", D, m, x, n]; null !== p && B.push(x, p, v, p); B.push(v, n, "Z"); a.shapeType = "path"; a.shapeArgs = { d: B }; a.percentage = 100 * C; a.plotX = g; a.plotY = (m + (p || n)) / 2; a.tooltipPos = [g, a.plotY]; a.dlBox = { x: v, y: m, topWidth: D - A, bottomWidth: x - v, height: Math.abs(w(p, n) - m), width: NaN }; a.slice = G; a.half = E; d && !1 === a.visible || (r += C)
                        }); J(e, "afterTranslate")
                }, sortByAngle: function (a) {
                    a.sort(function (a,
                        b) { return a.plotY - b.plotY })
                }, drawDataLabels: function () {
                    var a = this.data, b = this.options.dataLabels.distance, e, c = a.length; for (this.center[2] -= 2 * b; c--;) {
                        var f = a[c]; var h = (e = f.half) ? 1 : -1; var d = f.plotY; f.labelDistance = w(f.options.dataLabels && f.options.dataLabels.distance, b); this.maxLabelDistance = Math.max(f.labelDistance, this.maxLabelDistance || 0); var H = this.getX(d, e, f); f.labelPosition = {
                            natural: { x: 0, y: d }, "final": {}, alignment: e ? "right" : "left", connectorPosition: {
                                breakAt: { x: H + (f.labelDistance - 5) * h, y: d }, touchingSliceAt: {
                                    x: H +
                                        f.labelDistance * h, y: d
                                }
                            }
                        }
                    } q[this.options.dataLabels.inside ? "column" : "pie"].prototype.drawDataLabels.call(this)
                }, alignDataLabel: function (a, c, e, k, f) {
                    var h = a.series; k = h.options.reversed; var d = a.dlBox || a.shapeArgs, q = e.align, r = e.verticalAlign, w = ((h.options || {}).dataLabels || {}).inside, g = h.center[1]; h = h.getWidthAt((k ? 2 * g - a.plotY : a.plotY) - d.height / 2 + c.height); h = "middle" === r ? (d.topWidth - d.bottomWidth) / 4 : (h - d.bottomWidth) / 2; g = d.y; var l = d.x; "middle" === r ? g = d.y - d.height / 2 + c.height / 2 : "top" === r && (g = d.y - d.height +
                        c.height + e.padding); if ("top" === r && !k || "bottom" === r && k || "middle" === r) "right" === q ? l = d.x - e.padding + h : "left" === q && (l = d.x + e.padding - h); k = { x: l, y: k ? g - d.height : g, width: d.bottomWidth, height: d.height }; e.verticalAlign = "bottom"; w && !a.visible || b.Series.prototype.alignDataLabel.call(this, a, c, e, k, f); w && (!a.visible && a.dataLabel && (a.dataLabel.placed = !1), a.contrastColor && c.css({ color: a.contrastColor }))
                }
            }); E(b.Chart, "afterHideAllOverlappingLabels", function () {
                this.series.forEach(function (a) {
                a instanceof q.pie && a.placeDataLabels &&
                    !((a.options || {}).dataLabels || {}).inside && a.placeDataLabels()
                })
            }); c("pyramid", "funnel", { neckWidth: "0%", neckHeight: "0%", reversed: !0 }); ""
    }); c(b, "masters/modules/funnel.src.js", [], function () { })
});
//# sourceMappingURL=funnel.js.map