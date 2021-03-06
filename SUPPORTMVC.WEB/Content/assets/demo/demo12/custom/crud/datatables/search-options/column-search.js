var DatatablesSearchOptionsColumnSearch = function () {
    $.fn.dataTable.Api.register("column().title()", function () {
        return $(this.header()).text().trim()
    });
    return {
        init: function () {
            var t;
            t = $("#m_table_1").DataTable({
                responsive: !0,
                dom: "<'row'<'col-sm-12'tr>>\n\t\t\t<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>",
                lengthMenu: [5, 10, 25, 50],
                pageLength: 10,
                language: {
                    lengthMenu: "Display _MENU_"
                },
                searchDelay: 500,
                processing: !0,
                serverSide: !0,
                ajax: {
                    url: "https://keenthemes.com/metronic/themes/themes/metronic/dist/preview/inc/api/datatables/demos/server.php",
                    type: "POST",
                    data: {
                        columnsDef: ["RecordID", "OrderID", "Country", "ShipCity", "CompanyAgent", "ShipDate", "Status", "Type", "Actions"]
                    }
                },
                columns: [{
                    data: "RecordID"
                }, {
                    data: "OrderID"
                }, {
                    data: "Country"
                }, {
                    data: "ShipCity"
                }, {
                    data: "CompanyAgent"
                }, {
                    data: "ShipDate"
                }, {
                    data: "Status"
                }, {
                    data: "Type"
                }, {
                    data: "Actions"
                }],
                initComplete: function () {
                    var e = $('<tr class="filter"></tr>').appendTo($(t.table().header()));
                    this.api().columns().every(function () {
                        var a, n = function (e) {
                            var a = $.fn.dataTable.util.escapeRegex($(this).val());
                            t.column($(this).closest("th").index()).search(a || "", !1, !0)
                        };
                        switch (this.title()) {
                            case "RecordID":
                            case "OrderID":
                            case "ShipCity":
                            case "CompanyAgent":
                                a = $('<input type="text" class="form-control form-filter input-sm"/>');
                                break;
                            case "Country":
                                a = $('<select class="form-control form-filter input-sm" title="Select"><option value="">Select</option></select>'), this.data().unique().sort().each(function (t, e) {
                                    $(a).append('<option value="' + t + '">' + t + "</option>")
                                });
                                break;
                            case "Status":
                                var i = {
                                    1: {
                                        title: "Pending",
                                        class: "m-badge--brand"
                                    },
                                    2: {
                                        title: "Delivered",
                                        class: " m-badge--metal"
                                    },
                                    3: {
                                        title: "Canceled",
                                        class: " m-badge--primary"
                                    },
                                    4: {
                                        title: "Success",
                                        class: " m-badge--success"
                                    },
                                    5: {
                                        title: "Info",
                                        class: " m-badge--info"
                                    },
                                    6: {
                                        title: "Danger",
                                        class: " m-badge--danger"
                                    },
                                    7: {
                                        title: "Warning",
                                        class: " m-badge--warning"
                                    }
                                };
                                a = $('<select class="form-control form-filter input-sm" title="Select"><option value="">Select</option></select>'), this.data().unique().sort().each(function (t, e) {
                                    $(a).append('<option value="' + t + '">' + i[t].title + "</option>")
                                });
                                break;
                            case "Type":
                                i = {
                                    1: {
                                        title: "Online",
                                        state: "danger"
                                    },
                                    2: {
                                        title: "Retail",
                                        state: "primary"
                                    },
                                    3: {
                                        title: "Direct",
                                        state: "accent"
                                    }
                                }, a = $('<select class="form-control form-filter input-sm" title="Select"><option value="">Select</option></select>'), this.data().unique().sort().each(function (t, e) {
                                    $(a).append('<option value="' + t + '">' + i[t].title + "</option>")
                                });
                                break;
                            case "ShipDate":
                                a = $('\n\t\t\t\t\t\t\t<div class="input-group date">\n\t\t\t\t\t\t\t\t<input type="text" class="form-control m-input" readonly placeholder="From" id="m_datepicker_1"/>\n\t\t\t\t\t\t\t\t<div class="input-group-append">\n\t\t\t\t\t\t\t\t\t<span class="input-group-text"><i class="la la-calendar-o glyphicon-th"></i></span>\n\t\t\t\t\t\t\t\t</div>\n\t\t\t\t\t\t\t</div>\n\t\t\t\t\t\t\t<div class="input-group date">\n\t\t\t\t\t\t\t\t<input type="text" class="form-control m-input" readonly placeholder="To" id="m_datepicker_2"/>\n\t\t\t\t\t\t\t\t<div class="input-group-append">\n\t\t\t\t\t\t\t\t\t<span class="input-group-text"><i class="la la-calendar-o glyphicon-th"></i></span>\n\t\t\t\t\t\t\t\t</div>\n\t\t\t\t\t\t\t</div>');
                                break;
                            case "Actions":
                                var s = $('<button class="btn btn-brand m-btn m-btn--icon">\n\t\t\t\t\t\t\t  <span>\n\t\t\t\t\t\t\t    <i class="la la-search"></i>\n\t\t\t\t\t\t\t    <span>Search</span>\n\t\t\t\t\t\t\t  </span>\n\t\t\t\t\t\t\t</button>'),
                                    l = $('<button class="btn btn-secondary m-btn m-btn--icon">\n\t\t\t\t\t\t\t  <span>\n\t\t\t\t\t\t\t    <i class="la la-close"></i>\n\t\t\t\t\t\t\t    <span>Reset</span>\n\t\t\t\t\t\t\t  </span>\n\t\t\t\t\t\t\t</button>');
                                $("<th>").append(s).append(l).appendTo(e), $(s).on("click", function (a) {
                                    a.preventDefault(), $(e).find(":input").each(n), t.table().draw()
                                }), $(l).on("click", function (a) {
                                    a.preventDefault(), $(e).find(":input").each(function () {
                                        $(this).val("")
                                    }).each(n), t.table().draw()
                                })
                        }
                        $(a).appendTo($("<th>").appendTo(e))
                    }), $("#m_datepicker_1,#m_datepicker_2").datepicker()
                },
                columnDefs: [{
                    targets: -1,
                    title: "Actions",
                    orderable: !1,
                    render: function (t, e, a, n) {
                        return '\n                        <span class="dropdown">\n                            <a href="#" class="btn m-btn m-btn--hover-brand m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown" aria-expanded="true">\n                              <i class="la la-ellipsis-h"></i>\n                            </a>\n                            <div class="dropdown-menu dropdown-menu-right">\n                                <a class="dropdown-item" href="#"><i class="la la-edit"></i> Edit Details</a>\n                                <a class="dropdown-item" href="#"><i class="la la-leaf"></i> Update Status</a>\n                                <a class="dropdown-item" href="#"><i class="la la-print"></i> Generate Report</a>\n                            </div>\n                        </span>\n                        <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-brand m-btn--icon m-btn--icon-only m-btn--pill" title="View">\n                          <i class="la la-edit"></i>\n                        </a>'
                    }
                }, {
                    targets: 5,
                    width: "150px"
                }, {
                    targets: 6,
                    render: function (t, e, a, n) {
                        var i = {
                            1: {
                                title: "Pending",
                                class: "m-badge--brand"
                            },
                            2: {
                                title: "Delivered",
                                class: " m-badge--metal"
                            },
                            3: {
                                title: "Canceled",
                                class: " m-badge--primary"
                            },
                            4: {
                                title: "Success",
                                class: " m-badge--success"
                            },
                            5: {
                                title: "Info",
                                class: " m-badge--info"
                            },
                            6: {
                                title: "Danger",
                                class: " m-badge--danger"
                            },
                            7: {
                                title: "Warning",
                                class: " m-badge--warning"
                            }
                        };
                        return void 0 === i[t] ? t : '<span class="m-badge ' + i[t].class + ' m-badge--wide">' + i[t].title + "</span>"
                    }
                }, {
                    targets: 7,
                    render: function (t, e, a, n) {
                        var i = {
                            1: {
                                title: "Online",
                                state: "danger"
                            },
                            2: {
                                title: "Retail",
                                state: "primary"
                            },
                            3: {
                                title: "Direct",
                                state: "accent"
                            }
                        };
                        return void 0 === i[t] ? t : '<span class="m-badge m-badge--' + i[t].state + ' m-badge--dot"></span>&nbsp;<span class="m--font-bold m--font-' + i[t].state + '">' + i[t].title + "</span>"
                    }
                }]
            })
        }
    }
}();
jQuery(document).ready(function () {
    DatatablesSearchOptionsColumnSearch.init()
});