var DatatablesDataSourceHtml = {
    init: function () {
        $("#m_table_1").DataTable({
            responsive: !0,
            columnDefs: [{
                targets: -1,
                title: "Düzenle",
                orderable: !1,
                render: function (a, t, e, n) {
                    return '\n<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-brand m-btn--icon m-btn--icon-only m-btn--pill" title="Düzenle">\n<i class="la la-edit"></i>\n</a>'
                }
            }, {
                targets: 7,
                render: function (a, t, e, n) {
                    var s = {
                        1: {
                            title: "Görüldü",
                            class: "m-badge--brand"
                        },
                        2: {
                            title: "İptal Edildi",
                            class: " m-badge--metal"
                        },
                        3: {
                            title: "Ar-Ge Sürecinde",
                            class: " m-badge--primary"
                        },
                        4: {
                            title: "Aktif",
                            class: " m-badge--success"
                        },
                        5: {
                            title: "İnceleniyor",
                            class: " m-badge--info"
                        },
                        6: {
                            title: "Çözüldü",
                            class: " m-badge--danger"
                        },
                        7: {
                            title: "Cevap Bekleniyor",
                            class: " m-badge--warning"
                        }
                    };
                    return void 0 === s[a] ? a : '<span class="m-badge ' + s[a].class + ' m-badge--wide">' + s[a].title + "</span>"
                }
            }, {
                targets: 5,
                render: function (a, t, e, n) {
                    var s = {
                        1: {
                            title: "Acil",
                            state: "danger"
                        },
                        2: {
                            title: "Öncelikli",
                            state: "primary"
                        },
                        3: {
                            title: "Normal",
                            state: "accent"
                        }
                    };
                    return void 0 === s[a] ? a : '<span class="m-badge m-badge--' + s[a].state + ' m-badge--dot"></span>&nbsp;<span class="m--font-bold m--font-' + s[a].state + '">' + s[a].title + "</span>"
                }
            }]
        })
    }
};
jQuery(document).ready(function () {
    DatatablesDataSourceHtml.init()
});