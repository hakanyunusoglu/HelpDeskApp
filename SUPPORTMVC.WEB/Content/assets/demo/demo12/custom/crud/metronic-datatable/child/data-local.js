var dataTableField = [];




$.ajax({
    type: "POST",
    url: "/TalepListesi.aspx/GetRequestList",
    data: "{ }",
    contentType: 'application/json; charset=utf-8',
    dataType: 'json',
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
    },
    success: function (result) {

        dataTableField = JSON.parse(result.d);
        console.log(dataTableField);  
    }
});

var DatatableChildDataLocalDemo = function () {
    var e = function (e) {
        $("<div/>").attr("id", "child_data_local_" + e.data.ID).appendTo(e.detailCell).mDatatable({
            data: {
                type: "local",
                source: e.data.Orders,
                pageSize: 10,
            },
            layout: {
                theme: "default",
                scroll: !0,
                height: 300,
                footer: !1,
                spinner: {
                    theme: "default"
                }
            },
            sortable: !1,
            pagination: !1,
            columns: [{
                field: "TalepNo",
                title: "Talep No",
                sortable: !1,
                width: 60
            },
            {
                field: "Oncelik",
                title: "Öncelik",
                width: 100,
                template: function (e) {
                    var r = {
                        1: {
                            title: "Normal",
                            state: "brand"
                        },
                        2: {
                            title: "Öncelikli",
                            state: "warning"
                        },
                        3: {
                            title: "Acil",
                            state: "danger"
                        }
                    };
                    return '<span class= m-badge--' + r[e.Oncelik].state + ' m-badge--dot"></span>&nbsp;<span class="m--font-bold m--font-' + r[e.Oncelik].state + '">' + r[e.Oncelik].title + "</span>"
                }
            }, {
                field: "TalepDurumu",
                title: "Talep Durumu",
                sortable: !1,
                width: 180,
                template: function () {
                    return '\t\t\t\t\t\t <select runat="server" class="form-control m-input m--font-boldest2" id="txtaktiftalepdurumdegis">' +
                        '\t\t\t\t\t\t <option value = "" selected disabled hidden > Seçiniz </option >' +
                        '\t\t\t\t\t\t <option value="0">Aktif</option>' +
                        '\t\t\t\t\t\t <option value="1">Görüldü</option>' +
                        '\t\t\t\t\t\t <option value="2">İnceleniyor</option>' +
                        '\t\t\t\t\t\t <option value="3">Çözüldü</option>' +
                        '\t\t\t\t\t\t <option value="4">Cevap Bekleniyor</option>' +
                        '\t\t\t\t\t\t <option value="5">Ar-ge Sürecinde</option>' +
                        '\t\t\t\t\t\t <option value="6">İptal Edildi</option>' +
                        '\t\t\t\t\t\t </select >\t\t\t\t\t\t'
                }
            },
            {
                field: "Aciklama",
                title: "Açıklama",
                sortable: !1,
                width: 600,
                template: function () {
                    return '\t\t\t\t\t\t<textarea runat="server" class="form-control m-input m--font-boldest2" id="aktiftalepduzenleaciklama" rows="4" ClientIDMode="Static"></textarea>\t\t\t\t\t\t'
                }
            }, {
                field: "Gorevli",
                title: "Görevli",
                sortable: !1,
                width: 180,
                template: function () {
                    return '\t\t\t\t\t\t <select runat="server" class="form-control m-input m--font-boldest2" id="txtaktiftalepgorevlikisi">' +
                        '\t\t\t\t\t\t <option value = "" selected disabled hidden > Seçiniz </option >' +
                        '\t\t\t\t\t\t <option value="0">Hakan Yunusoğlu</option>' +
                        '\t\t\t\t\t\t </select >\t\t\t\t\t\t'
                }
            },
            {
                field: "TalepGuncelle",
                title: "",
                sortable: !1,
                template: function () {
                    return '\t\t\t\t\t\t <button runat="server" type="button" class="btn btn-info m--font-bolder" id="btntalepguncelle">Güncelle</button>\t\t\t\t\t\t'
                }
            }
            ]
        })
    };
    return {
        init: function () {

          
            var r;

            r = dataTableField;
            $(".m_datatable").mDatatable({
                    data: {
                        type: "local",
                        source: r,
                        pageSize: 10,
                        width: 150
                    },
                    layout: {
                        theme: "default",
                        scroll: !1,
                        height: null,
                        footer: !1
                    },
                    sortable: !1,
                    filterable: !1,
                    pagination: !1,
                    detail: {
                        title: "İçerik Yükleniyor",
                        content: e
                    },
                    search: {
                        input: $("#generalSearch")
                    },
                    columns: [
                        {
                            field: "ID",
                            title: "",
                            sortable: !1,
                            width: 20,
                            textAlign: "center"
                        }, {
                            field: "TalepTarihi",
                            title: "Talep Tarihi"
                        }, {
                            field: "FirmaAdi",
                            title: "Firma Adı",
                            width: 170,
                        }, {
                            field: "TalepSahibi",
                            title: "Talep Sahibi"
                        }, {
                            field: "TalepNo",
                            title: "Talep No"
                        }, {
                            field: "TalepKonu",
                            title: "Konu"
                        },
                        {
                            field: "Oncelik",
                            title: "Öncelik",
                            template: function(e) {
                                var r = {
                                    1: {
                                        title: "Normal",
                                        state: "brand"
                                    },
                                    2: {
                                        title: "Öncelikli",
                                        state: "warning"
                                    },
                                    3: {
                                        title: "Acil",
                                        state: "danger"
                                    }
                                };
                                return '<span class="m-badge m-badge--' +
                                    r[e.Oncelik].state +
                                    ' m-badge--dot"></span>&nbsp;<span class="m--font-bold m--font-' +
                                    r[e.Oncelik].state +
                                    '">' +
                                    r[e.Oncelik].title +
                                    "</span>"
                            }
                        }, {
                            field: "TalepDurumu",
                            title: "Talep Durumu",
                            template: function(e) {
                                var r = {
                                    1: {
                                        title: "Ar-Ge Sürecinde",
                                        class: "m-badge--brand"
                                    },
                                    2: {
                                        title: "Çözüldü",
                                        class: " m-badge--metal"
                                    },
                                    3: {
                                        title: "Cevap Bekleniyor",
                                        class: " m-badge--primary"
                                    },
                                    4: {
                                        title: "Aktif",
                                        class: " m-badge--success"
                                    },
                                    5: {
                                        title: "Görüldü",
                                        class: " m-badge--info"
                                    },
                                    6: {
                                        title: "İptal Edildi",
                                        class: " m-badge--danger"
                                    },
                                    7: {
                                        title: "İnceleniyor",
                                        class: " m-badge--warning"
                                    }
                                };
                                return '<span class="m-badge ' +
                                    r[e.TalepDurumu].class +
                                    ' m-badge--wide">' +
                                    r[e.TalepDurumu].title +
                                    "</span>"
                            }
                        }, {
                            field: "Duzenle",
                            width: 110,
                            title: "Düzenle",
                            sortable: !1,
                            overflow: "visible",
                            template: function() {
                                return '\t\t\t\t\t\t<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="Edit details">\t\t\t\t\t\t\t<i class="la la-edit"></i>\t\t\t\t\t\t</a>\t\t\t\t\t\t<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Delete">\t\t\t\t\t\t\t<i class="la la-trash"></i>\t\t\t\t\t\t</a>\t\t\t\t\t'
                            }
                        }
                    ]
                });
        }
    }
}();
jQuery(document).ready(function () {

    //for (var i = 1; i < 30; i++) {
    //    dataTableObject = {};
    //    subDataTableObject = {};
    //    subDataTableObjectOrder = [];
    //    dataTableObject.ID = i;
    //    dataTableObject.TalepTarihi = "18/02/2019";
    //    dataTableObject.FirmaAdi = "FirmaAdi" + i;
    //    dataTableObject.TalepSahibi = "TalepSahibi" + i;
    //    dataTableObject.TalepNo = i;
    //    dataTableObject.TalepKonu = "TalepKonu" + i;
    //    dataTableObject.Oncelik = 2;
    //    dataTableObject.TalepDurumu = 3;
    //    subDataTableObject.TalepNo = i;
    //    subDataTableObject.Oncelik = 2;
    //    subDataTableObjectOrder.push(subDataTableObject);
    //    dataTableObject.Orders = subDataTableObjectOrder;
    //    dataTableField.push(dataTableObject);
    //}

    DatatableChildDataLocalDemo.init();


});