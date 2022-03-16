var FlotchartsDemo = function () {
    var t = function () {
        function t() {
            return Math.floor(21 * Math.random()) + 20
        }
        var e = [
            [1, t()],
            [2, t()],
            [3, 2 + t()],
            [4, 3 + t()],
            [5, 5 + t()],
            [6, 10 + t()],
            [7, 15 + t()],
            [8, 20 + t()],
            [9, 25 + t()],
            [10, 30 + t()],
            [11, 35 + t()],
            [12, 25 + t()],
            [13, 15 + t()],
            [14, 20 + t()],
            [15, 45 + t()],
            [16, 50 + t()],
            [17, 65 + t()],
            [18, 70 + t()],
            [19, 85 + t()],
            [20, 80 + t()],
            [21, 75 + t()],
            [22, 80 + t()],
            [23, 75 + t()],
            [24, 70 + t()],
            [25, 65 + t()],
            [26, 75 + t()],
            [27, 80 + t()],
            [28, 85 + t()],
            [29, 90 + t()],
            [30, 95 + t()]
        ],
            o = [
                [1, t() - 5],
                [2, t() - 5],
                [3, t() - 5],
                [4, 6 + t()],
                [5, 5 + t()],
                [6, 20 + t()],
                [7, 25 + t()],
                [8, 36 + t()],
                [9, 26 + t()],
                [10, 38 + t()],
                [11, 39 + t()],
                [12, 50 + t()],
                [13, 51 + t()],
                [14, 12 + t()],
                [15, 13 + t()],
                [16, 14 + t()],
                [17, 15 + t()],
                [18, 15 + t()],
                [19, 16 + t()],
                [20, 17 + t()],
                [21, 18 + t()],
                [22, 19 + t()],
                [23, 20 + t()],
                [24, 21 + t()],
                [25, 14 + t()],
                [26, 24 + t()],
                [27, 25 + t()],
                [28, 26 + t()],
                [29, 27 + t()],
                [30, 31 + t()]
            ];
        $.plot($("#m_flotcharts_2"), [{
            data: e,
            label: "Unique Visits",
            lines: {
                lineWidth: 1
            },
            shadowSize: 0
        }, {
            data: o,
            label: "Page Views",
            lines: {
                lineWidth: 1
            },
            shadowSize: 0
        }], {
                series: {
                    lines: {
                        show: !0,
                        lineWidth: 2,
                        fill: !0,
                        fillColor: {
                            colors: [{
                                opacity: .05
                            }, {
                                opacity: .01
                            }]
                        }
                    },
                    points: {
                        show: !0,
                        radius: 3,
                        lineWidth: 1
                    },
                    shadowSize: 2
                },
                grid: {
                    hoverable: !0,
                    clickable: !0,
                    tickColor: "#eee",
                    borderColor: "#eee",
                    borderWidth: 1
                },
                colors: ["#d12610", "#37b7f3", "#52e136"],
                xaxis: {
                    ticks: 11,
                    tickDecimals: 0,
                    tickColor: "#eee"
                },
                yaxis: {
                    ticks: 11,
                    tickDecimals: 0,
                    tickColor: "#eee"
                }
            });
        var a = null;
        $("#chart_2").bind("plothover", function (t, e, o) {
            if ($("#x").text(e.x.toFixed(2)), $("#y").text(e.y.toFixed(2)), o) {
                if (a != o.dataIndex) {
                    a = o.dataIndex, $("#tooltip").remove();
                    var i = o.datapoint[0].toFixed(2),
                        r = o.datapoint[1].toFixed(2);
                    ! function (t, e, o) {
                        $('<div id="tooltip">' + o + "</div>").css({
                            position: "absolute",
                            display: "none",
                            top: e + 5,
                            left: t + 15,
                            border: "1px solid #333",
                            padding: "4px",
                            color: "#fff",
                            "border-radius": "3px",
                            "background-color": "#333",
                            opacity: .8
                        }).appendTo("body").fadeIn(200)
                    }(o.pageX, o.pageY, o.series.label + " of " + i + " = " + r)
                }
            } else $("#tooltip").remove(), a = null
        })
    };
    return {
        init: function () {
            ! function () {
                for (var t = [], e = 0; e < 2 * Math.PI; e += .25) t.push([e, Math.sin(e)]);
                var o = [];
                for (e = 0; e < 2 * Math.PI; e += .25) o.push([e, Math.cos(e)]);
                var a = [];
                for (e = 0; e < 2 * Math.PI; e += .1) a.push([e, Math.tan(e)]);
                $.plot($("#m_flotcharts_1"), [{
                    label: "sin(x)",
                    data: t,
                    lines: {
                        lineWidth: 1
                    },
                    shadowSize: 0
                }, {
                    label: "cos(x)",
                    data: o,
                    lines: {
                        lineWidth: 1
                    },
                    shadowSize: 0
                }, {
                    label: "tan(x)",
                    data: a,
                    lines: {
                        lineWidth: 1
                    },
                    shadowSize: 0
                }], {
                        series: {
                            lines: {
                                show: !0
                            },
                            points: {
                                show: !0,
                                fill: !0,
                                radius: 3,
                                lineWidth: 1
                            }
                        },
                        xaxis: {
                            tickColor: "#eee",
                            ticks: [0, [Math.PI / 2, "π/2"],
                                [Math.PI, "π"],
                                [3 * Math.PI / 2, "3π/2"],
                                [2 * Math.PI, "2π"]
                            ]
                        },
                        yaxis: {
                            tickColor: "#eee",
                            ticks: 10,
                            min: -2,
                            max: 2
                        },
                        grid: {
                            borderColor: "#eee",
                            borderWidth: 1
                        }
                    })
            }(), t(),
                function () {
                    for (var t = [], e = [], o = 0; o < 14; o += .1) t.push([o, Math.sin(o)]), e.push([o, Math.cos(o)]);
                    plot = $.plot($("#m_flotcharts_3"), [{
                        data: t,
                        label: "sin(x) = -0.00",
                        lines: {
                            lineWidth: 1
                        },
                        shadowSize: 0
                    }, {
                        data: e,
                        label: "cos(x) = -0.00",
                        lines: {
                            lineWidth: 1
                        },
                        shadowSize: 0
                    }], {
                            series: {
                                lines: {
                                    show: !0
                                }
                            },
                            crosshair: {
                                mode: "x"
                            },
                            grid: {
                                hoverable: !0,
                                autoHighlight: !1,
                                tickColor: "#eee",
                                borderColor: "#eee",
                                borderWidth: 1
                            },
                            yaxis: {
                                min: -1.2,
                                max: 1.2
                            }
                        });
                    var a = $("#m_flotcharts_3 .legendLabel");
                    a.each(function () {
                        $(this).css("width", $(this).width())
                    });
                    var i = null,
                        r = null;

                    function l() {
                        i = null;
                        var t = r,
                            e = plot.getAxes();
                        if (!(t.x < e.xaxis.min || t.x > e.xaxis.max || t.y < e.yaxis.min || t.y > e.yaxis.max)) {
                            var o, l, n = plot.getData();
                            for (o = 0; o < n.length; ++o) {
                                var s = n[o];
                                for (l = 0; l < s.data.length && !(s.data[l][0] > t.x); ++l);
                                var h, d = s.data[l - 1],
                                    c = s.data[l];
                                h = null == d ? c[1] : null == c ? d[1] : d[1] + (c[1] - d[1]) * (t.x - d[0]) / (c[0] - d[0]), a.eq(o).text(s.label.replace(/=.*/, "= " + h.toFixed(2)))
                            }
                        }
                    }
                    $("#m_flotcharts_3").bind("plothover", function (t, e, o) {
                        r = e, i || (i = setTimeout(l, 50))
                    })
                }(),
                function () {
                    var t = [],
                        e = 250;

                    function o() {
                        for (t.length > 0 && (t = t.slice(1)); t.length < e;) {
                            var o = (t.length > 0 ? t[t.length - 1] : 50) + 10 * Math.random() - 5;
                            o < 0 && (o = 0), o > 100 && (o = 100), t.push(o)
                        }
                        for (var a = [], i = 0; i < t.length; ++i) a.push([i, t[i]]);
                        return a
                    }
                    var a = 30,
                        i = $.plot($("#m_flotcharts_4"), [o()], {
                            series: {
                                shadowSize: 1
                            },
                            lines: {
                                show: !0,
                                lineWidth: .5,
                                fill: !0,
                                fillColor: {
                                    colors: [{
                                        opacity: .1
                                    }, {
                                        opacity: 1
                                    }]
                                }
                            },
                            yaxis: {
                                min: 0,
                                max: 100,
                                tickColor: "#eee",
                                tickFormatter: function (t) {
                                    return t + "%"
                                }
                            },
                            xaxis: {
                                show: !1
                            },
                            colors: ["#6ef146"],
                            grid: {
                                tickColor: "#eee",
                                borderWidth: 0
                            }
                        });
                    ! function t() {
                        i.setData([o()]), i.draw(), setTimeout(t, a)
                    }()
                }(),
                function () {
                    for (var t = [], e = 0; e <= 10; e += 1) t.push([e, parseInt(30 * Math.random())]);
                    var o = [];
                    for (e = 0; e <= 10; e += 1) o.push([e, parseInt(30 * Math.random())]);
                    var a = [];
                    for (e = 0; e <= 10; e += 1) a.push([e, parseInt(30 * Math.random())]);
                    var i = 0,
                        r = !0,
                        l = !1,
                        n = !1;

                    function s() {
                        $.plot($("#m_flotcharts_5"), [{
                            label: "sales",
                            data: t,
                            lines: {
                                lineWidth: 1
                            },
                            shadowSize: 0
                        }, {
                            label: "tax",
                            data: o,
                            lines: {
                                lineWidth: 1
                            },
                            shadowSize: 0
                        }, {
                            label: "profit",
                            data: a,
                            lines: {
                                lineWidth: 1
                            },
                            shadowSize: 0
                        }], {
                                series: {
                                    stack: i,
                                    lines: {
                                        show: l,
                                        fill: !0,
                                        steps: n,
                                        lineWidth: 0
                                    },
                                    bars: {
                                        show: r,
                                        barWidth: .5,
                                        lineWidth: 0,
                                        shadowSize: 0,
                                        align: "center"
                                    }
                                },
                                grid: {
                                    tickColor: "#eee",
                                    borderColor: "#eee",
                                    borderWidth: 1
                                }
                            })
                    }
                    $(".stackControls input").click(function (t) {
                        t.preventDefault(), i = "With stacking" == $(this).val() || null, s()
                    }), $(".graphControls input").click(function (t) {
                        t.preventDefault(), r = -1 != $(this).val().indexOf("Bars"), l = -1 != $(this).val().indexOf("Lines"), n = -1 != $(this).val().indexOf("steps"), s()
                    }), s()
                }(),
                function () {
                    var t = function (t) {
                        var e = [],
                            o = 100 + t,
                            a = 200 + t;
                        for (i = 1; i <= 20; i++) {
                            var r = Math.floor(Math.random() * (a - o + 1) + o);
                            e.push([i, r]), o++ , a++
                        }
                        return e
                    }(0);
                    $.plot($("#m_flotcharts_6"), [{
                        data: t,
                        lines: {
                            lineWidth: 1
                        },
                        shadowSize: 0
                    }], {
                            series: {
                                bars: {
                                    show: !0
                                }
                            },
                            bars: {
                                barWidth: .8,
                                lineWidth: 0,
                                shadowSize: 0,
                                align: "left"
                            },
                            grid: {
                                tickColor: "#eee",
                                borderColor: "#eee",
                                borderWidth: 1
                            }
                        })
                }(), $.plot($("#m_flotcharts_7"), [
                    [
                        [10, 10],
                        [20, 20],
                        [30, 30],
                        [40, 40],
                        [50, 50]
                    ]
                ], {
                        series: {
                            bars: {
                                show: !0
                            }
                        },
                        bars: {
                            horizontal: !0,
                            barWidth: 6,
                            lineWidth: 0,
                            shadowSize: 0,
                            align: "left"
                        },
                        grid: {
                            tickColor: "#eee",
                            borderColor: "#eee",
                            borderWidth: 1
                        }
                    }),
                function () {
                    var t = [],
                        e = Math.floor(10 * Math.random()) + 1;
                    e = e < 5 ? 5 : e;
                    for (var o = 0; o < e; o++) t[o] = {
                        label: "Series" + (o + 1),
                        data: Math.floor(100 * Math.random()) + 1
                    };
                    $.plot($("#m_flotcharts_8"), t, {
                        series: {
                            pie: {
                                show: !0
                            }
                        }
                    })
                }(),
                function () {
                    var t = [],
                        e = Math.floor(10 * Math.random()) + 1;
                    e = e < 5 ? 5 : e;
                    for (var o = 0; o < e; o++) t[o] = {
                        label: "Series" + (o + 1),
                        data: Math.floor(100 * Math.random()) + 1
                    };
                    $.plot($("#m_flotcharts_9"), t, {
                        series: {
                            pie: {
                                show: !0
                            }
                        },
                        legend: {
                            show: !1
                        }
                    })
                }(),
                function () {
                    var t = [],
                        e = Math.floor(10 * Math.random()) + 1;
                    e = e < 5 ? 5 : e;
                    for (var o = 0; o < e; o++) t[o] = {
                        label: "Series" + (o + 1),
                        data: Math.floor(100 * Math.random()) + 1
                    };
                    $.plot($("#m_flotcharts_10"), t, {
                        series: {
                            pie: {
                                show: !0,
                                radius: 1,
                                label: {
                                    show: !0,
                                    radius: 1,
                                    formatter: function (t, e) {
                                        return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">' + t + "<br/>" + Math.round(e.percent) + "%</div>"
                                    },
                                    background: {
                                        opacity: .8
                                    }
                                }
                            }
                        },
                        legend: {
                            show: !1
                        }
                    })
                }(),
                function () {
                    var t = [],
                        e = Math.floor(10 * Math.random()) + 1;
                    e = e < 5 ? 5 : e;
                    for (var o = 0; o < e; o++) t[o] = {
                        label: "Series" + (o + 1),
                        data: Math.floor(100 * Math.random()) + 1
                    };
                    $.plot($("#m_flotcharts_11"), t, {
                        series: {
                            pie: {
                                show: !0,
                                radius: 1,
                                label: {
                                    show: !0,
                                    radius: 1,
                                    formatter: function (t, e) {
                                        return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">' + t + "<br/>" + Math.round(e.percent) + "%</div>"
                                    },
                                    background: {
                                        opacity: .8
                                    }
                                }
                            }
                        },
                        legend: {
                            show: !1
                        }
                    })
                }()
        }
    }
}();
jQuery(document).ready(function () {
    FlotchartsDemo.init()
});