/*
 Highcharts JS v8.0.0 (2019-12-10)

 Highcharts funnel module

 (c) 2010-2019 Kacper Madej

 License: www.highcharts.com/license
*/
(function (c) { "object" === typeof module && module.exports ? (c["default"] = c, module.exports = c) : "function" === typeof define && define.amd ? define("highcharts/modules/funnel3d", ["highcharts", "highcharts/highcharts-3d", "highcharts/modules/cylinder"], function (n) { c(n); c.Highcharts = n; return c }) : c("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (c) {
    function n(e, c, n, x) { e.hasOwnProperty(c) || (e[c] = x.apply(null, n)) } c = c ? c._modules : {}; n(c, "modules/funnel3d.src.js", [c["parts/Globals.js"], c["parts/Utilities.js"]],
        function (e, c) {
            var n = c.extend, x = c.pick, B = c.relativeLength, G = e.charts, p = e.color, J = e.error, H = e.merge, y = e.seriesType, K = e.seriesTypes; c = e.Renderer.prototype; var L = c.cuboidPath; y("funnel3d", "column", { center: ["50%", "50%"], width: "90%", neckWidth: "30%", height: "100%", neckHeight: "25%", reversed: !1, gradientForSides: !0, animation: !1, edgeWidth: 0, colorByPoint: !0, showInLegend: !1, dataLabels: { align: "right", crop: !1, inside: !1, overflow: "allow" } }, {
                bindAxes: function () {
                    e.Series.prototype.bindAxes.apply(this, arguments); n(this.xAxis.options,
                        { gridLineWidth: 0, lineWidth: 0, title: null, tickPositions: [] }); n(this.yAxis.options, { gridLineWidth: 0, title: null, labels: { enabled: !1 } })
                }, translate3dShapes: e.noop, translate: function () {
                    e.Series.prototype.translate.apply(this, arguments); var a = 0, b = this.chart, d = this.options, h = d.reversed, I = d.ignoreHiddenPoint, f = b.plotWidth, c = b.plotHeight, g = 0, w = d.center, u = B(w[0], f), q = B(w[1], c), r = B(d.width, f), l, t, k = B(d.height, c), p = B(d.neckWidth, f), y = B(d.neckHeight, c), C = q - k / 2 + k - y; f = this.data; var z, E, v, A, F, D, m; this.getWidthAt = t =
                        function (b) { var a = q - k / 2; return b > C || k === y ? p : p + (r - p) * (1 - (b - a) / (k - y)) }; this.center = [u, q, k]; this.centerX = u; f.forEach(function (b) { I && !1 === b.visible || (a += b.y) }); f.forEach(function (f) {
                            F = null; z = a ? f.y / a : 0; v = q - k / 2 + g * k; A = v + z * k; l = t(v); D = A - v; m = { gradientForSides: x(f.options.gradientForSides, d.gradientForSides), x: u, y: v, height: D, width: l, z: 1, top: { width: l } }; l = t(A); m.bottom = { fraction: z, width: l }; v >= C ? m.isCylinder = !0 : A > C && (F = A, l = t(C), A = C, m.bottom.width = l, m.middle = { fraction: D ? (C - v) / D : 0, width: l }); h && (m.y = v = q + k / 2 - (g + z) *
                                k, m.middle && (m.middle.fraction = 1 - (D ? m.middle.fraction : 0)), l = m.width, m.width = m.bottom.width, m.bottom.width = l); f.shapeArgs = n(f.shapeArgs, m); f.percentage = 100 * z; f.plotX = u; f.plotY = h ? q + k / 2 - (g + z / 2) * k : (v + (F || A)) / 2; E = e.perspective([{ x: u, y: f.plotY, z: h ? -(r - t(f.plotY)) / 2 : -t(f.plotY) / 2 }], b, !0)[0]; f.tooltipPos = [E.x, E.y]; f.dlBoxRaw = { x: u, width: t(f.plotY), y: v, bottom: m.height, fullWidth: r }; I && !1 === f.visible || (g += z)
                        })
                }, alignDataLabel: function (a, b, d) {
                    var h = a.dlBoxRaw, c = this.chart.inverted, f = a.plotY > x(this.translatedThreshold,
                        this.yAxis.len), e = x(d.inside, !!this.options.stacking), g = { x: h.x, y: h.y, height: 0 }; d.align = x(d.align, !c || e ? "center" : f ? "right" : "left"); d.verticalAlign = x(d.verticalAlign, c || e ? "middle" : f ? "top" : "bottom"); "top" !== d.verticalAlign && (g.y += h.bottom / ("bottom" === d.verticalAlign ? 1 : 2)); g.width = this.getWidthAt(g.y); this.options.reversed && (g.width = h.fullWidth - g.width); e ? g.x -= g.width / 2 : "left" === d.align ? (d.align = "right", g.x -= 1.5 * g.width) : "right" === d.align ? (d.align = "left", g.x += g.width / 2) : g.x -= g.width / 2; a.dlBox = g; K.column.prototype.alignDataLabel.apply(this,
                            arguments)
                }
            }, { shapeType: "funnel3d", hasNewShapeType: e.seriesTypes.column.prototype.pointClass.prototype.hasNewShapeType }); y = e.merge(c.elements3d.cuboid, {
                parts: "top bottom frontUpper backUpper frontLower backLower rightUpper rightLower".split(" "), mainParts: ["top", "bottom"], sideGroups: ["upperGroup", "lowerGroup"], sideParts: { upperGroup: ["frontUpper", "backUpper", "rightUpper"], lowerGroup: ["frontLower", "backLower", "rightLower"] }, pathType: "funnel3d", opacitySetter: function (a) {
                    var b = this, d = b.parts, h = e.charts[b.renderer.chartIndex],
                    c = "group-opacity-" + a + "-" + h.index; b.parts = b.mainParts; b.singleSetterForParts("opacity", a); b.parts = d; h.renderer.filterId || (h.renderer.definition({ tagName: "filter", id: c, children: [{ tagName: "feComponentTransfer", children: [{ tagName: "feFuncA", type: "table", tableValues: "0 " + a }] }] }), b.sideGroups.forEach(function (a) { b[a].attr({ filter: "url(#" + c + ")" }) }), b.renderer.styledMode && (h.renderer.definition({ tagName: "style", textContent: ".highcharts-" + c + " {filter:url(#" + c + ")}" }), b.sideGroups.forEach(function (b) {
                        b.addClass("highcharts-" +
                            c)
                    }))); return b
                }, fillSetter: function (a) {
                    var b = this, d = p(a), c = d.rgba[3], e = { top: p(a).brighten(.1).get(), bottom: p(a).brighten(-.2).get() }; 1 > c ? (d.rgba[3] = 1, d = d.get("rgb"), b.attr({ opacity: c })) : d = a; d.linearGradient || d.radialGradient || !b.gradientForSides || (d = { linearGradient: { x1: 0, x2: 1, y1: 1, y2: 1 }, stops: [[0, p(a).brighten(-.2).get()], [.5, a], [1, p(a).brighten(-.2).get()]] }); d.linearGradient ? b.sideGroups.forEach(function (a) {
                        var c = b[a].gradientBox, f = d.linearGradient, h = H(d, {
                            linearGradient: {
                                x1: c.x + f.x1 * c.width, y1: c.y +
                                    f.y1 * c.height, x2: c.x + f.x2 * c.width, y2: c.y + f.y2 * c.height
                            }
                        }); b.sideParts[a].forEach(function (b) { e[b] = h })
                    }) : (H(!0, e, { frontUpper: d, backUpper: d, rightUpper: d, frontLower: d, backLower: d, rightLower: d }), d.radialGradient && b.sideGroups.forEach(function (a) { var d = b[a].gradientBox, c = d.x + d.width / 2, f = d.y + d.height / 2, h = Math.min(d.width, d.height); b.sideParts[a].forEach(function (a) { b[a].setRadialReference([c, f, h]) }) })); b.singleSetterForParts("fill", null, e); b.color = b.fill = a; d.linearGradient && [b.frontLower, b.frontUpper].forEach(function (a) {
                    (a =
                        (a = a.element) && b.renderer.gradients[a.gradient]) && "userSpaceOnUse" !== a.attr("gradientUnits") && a.attr({ gradientUnits: "userSpaceOnUse" })
                    }); return b
                }, adjustForGradient: function () {
                    var a = this, b; a.sideGroups.forEach(function (d) {
                        var c = { x: Number.MAX_VALUE, y: Number.MAX_VALUE }, e = { x: -Number.MAX_VALUE, y: -Number.MAX_VALUE }; a.sideParts[d].forEach(function (d) { b = a[d].getBBox(!0); c = { x: Math.min(c.x, b.x), y: Math.min(c.y, b.y) }; e = { x: Math.max(e.x, b.x + b.width), y: Math.max(e.y, b.y + b.height) } }); a[d].gradientBox = {
                            x: c.x, width: e.x -
                                c.x, y: c.y, height: e.y - c.y
                        }
                    })
                }, zIndexSetter: function () { this.finishedOnAdd && this.adjustForGradient(); return this.renderer.Element.prototype.zIndexSetter.apply(this, arguments) }, onAdd: function () { this.adjustForGradient(); this.finishedOnAdd = !0 }
            }); c.elements3d.funnel3d = y; c.funnel3d = function (a) {
                var b = this.element3d("funnel3d", a), d = this.styledMode, c = { "stroke-width": 1, stroke: "none" }; b.upperGroup = this.g("funnel3d-upper-group").attr({ zIndex: b.frontUpper.zIndex }).add(b);[b.frontUpper, b.backUpper, b.rightUpper].forEach(function (a) {
                    d ||
                    a.attr(c); a.add(b.upperGroup)
                }); b.lowerGroup = this.g("funnel3d-lower-group").attr({ zIndex: b.frontLower.zIndex }).add(b);[b.frontLower, b.backLower, b.rightLower].forEach(function (a) { d || a.attr(c); a.add(b.lowerGroup) }); b.gradientForSides = a.gradientForSides; return b
            }; c.funnel3dPath = function (a) {
            this.getCylinderEnd || J("A required Highcharts module is missing: cylinder.js", !0, G[this.chartIndex]); var b = G[this.chartIndex], c = a.alphaCorrection = 90 - Math.abs(b.options.chart.options3d.alpha % 180 - 90), h = L.call(this, e.merge(a,
                { depth: a.width, width: (a.width + a.bottom.width) / 2 })), n = h.isTop, f = !h.isFront, p = !!a.middle, g = this.getCylinderEnd(b, e.merge(a, { x: a.x - a.width / 2, z: a.z - a.width / 2, alphaCorrection: c })), w = a.bottom.width, u = e.merge(a, { width: w, x: a.x - w / 2, z: a.z - w / 2, alphaCorrection: c }), q = this.getCylinderEnd(b, u, !0), r = w, l = u, t = q, k = q; p && (r = a.middle.width, l = e.merge(a, { y: a.y + a.middle.fraction * a.height, width: r, x: a.x - r / 2, z: a.z - r / 2 }), t = this.getCylinderEnd(b, l, !1), k = this.getCylinderEnd(b, l, !1)); h = {
                    top: g, bottom: q, frontUpper: this.getCylinderFront(g,
                        t), zIndexes: { group: h.zIndexes.group, top: 0 !== n ? 0 : 3, bottom: 1 !== n ? 0 : 3, frontUpper: f ? 2 : 1, backUpper: f ? 1 : 2, rightUpper: f ? 2 : 1 }
                }; h.backUpper = this.getCylinderBack(g, t); g = 1 !== Math.min(r, a.width) / Math.max(r, a.width); h.rightUpper = this.getCylinderFront(this.getCylinderEnd(b, e.merge(a, { x: a.x - a.width / 2, z: a.z - a.width / 2, alphaCorrection: g ? -c : 0 }), !1), this.getCylinderEnd(b, e.merge(l, { alphaCorrection: g ? -c : 0 }), !p)); p && (g = 1 !== Math.min(r, w) / Math.max(r, w), e.merge(!0, h, {
                    frontLower: this.getCylinderFront(k, q), backLower: this.getCylinderBack(k,
                        q), rightLower: this.getCylinderFront(this.getCylinderEnd(b, e.merge(u, { alphaCorrection: g ? -c : 0 }), !0), this.getCylinderEnd(b, e.merge(l, { alphaCorrection: g ? -c : 0 }), !1)), zIndexes: { frontLower: f ? 2 : 1, backLower: f ? 1 : 2, rightLower: f ? 1 : 2 }
                })); return h
            }
        }); n(c, "masters/modules/funnel3d.src.js", [], function () { })
});
//# sourceMappingURL=funnel3d.js.map