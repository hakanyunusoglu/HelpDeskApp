var DatatablesSearchOptionsAdvancedSearch = function () {
    $.fn.dataTable.Api.register("column().title()", function () {
        return $(this.header()).text().trim()
    });
    return {
        init: function () {
            var a;
            a = $("#m_table_1").DataTable({
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
                columnDefs: [{
                    targets: -1,
                    title: "Actions",
                    orderable: !1,
                    render: function (a, t, e, n) {
                        return '\n                        <span class="dropdown">\n                            <a href="#" class="btn m-btn m-btn--hover-brand m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown" aria-expanded="true">\n                              <i class="la la-ellipsis-h"></i>\n                            </a>\n                            <div class="dropdown-menu dropdown-menu-right">\n                                <a class="dropdown-item" href="#"><i class="la la-edit"></i> Edit Details</a>\n                                <a class="dropdown-item" href="#"><i class="la la-leaf"></i> Update Status</a>\n                                <a class="dropdown-item" href="#"><i class="la la-print"></i> Generate Report</a>\n                            </div>\n                        </span>\n                        <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-brand m-btn--icon m-btn--icon-only m-btn--pill" title="View">\n                          <i class="la la-edit"></i>\n                        </a>'
                    }
                }, {
                    targets: 6,
                    render: function (a, t, e, n) {
                        var s = {
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
                        return void 0 === s[a] ? a : '<span class="m-badge ' + s[a].class + ' m-badge--wide">' + s[a].title + "</span>"
                    }
                }, {
                    targets: 7,
                    render: function (a, t, e, n) {
                        var s = {
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
                        return void 0 === s[a] ? a : '<span class="m-badge m-badge--' + s[a].state + ' m-badge--dot"></span>&nbsp;<span class="m--font-bold m--font-' + s[a].state + '">' + s[a].title + "</span>"
                    }
                }]
            }), $("#m_form_status, #m_form_type").on("change", function () {
                var t = $.fn.dataTable.util.escapeRegex($(this).val());
                a.column($(this).data("idx")).search(t || "", !1, !1).draw()
            }).selectpicker()
        }
    }
}();
jQuery(document).ready(function () {
    DatatablesSearchOptionsAdvancedSearch.init()
});