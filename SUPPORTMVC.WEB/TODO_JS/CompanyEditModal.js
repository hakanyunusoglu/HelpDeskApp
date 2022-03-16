var comID = -1;
var modalbody = "#m_modal_4_body";

$(function () {

    $('#m_modal_4').on('show.bs.modal',
        function (e) {
            var btn = $(e.relatedTarget);
            comID = btn.data("company-id");
            $(modalbody).load("/Company/ShowCompanyEditModal/" + comID);
        });

});

function EditCompany(btn, mode) {
    if (mode === 'companyEdit') {
        var formData = new FormData();
        formData.append('CompanyCode', $('#CompanyCode').val());
        formData.append('CompanyName', $('#CompanyName').val());
        formData.append('CompanyAuthName', $('#CompanyAuthName').val());
        formData.append('CompanyAuthSurname', $('#CompanyAuthSurname').val());
        formData.append('CompanyMobilePhone', $('#CompanyMobilePhone').val());
        formData.append('CompanyOfficePhone', $('#CompanyOfficePhone').val());
        formData.append('CompanyEmail', $('#CompanyEmail').val());
        formData.append('CompanyDomain', $('#CompanyDomain').val());
        $.ajax({
            type: "POST",
            url: '/Company/EditCompany/' + comID,
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false,
            success: function (d) {
                if (d.result) {
                    swal({
                        type: "success",
                        title: "Başarılı!",
                        text: "Firma bilgileri güncellendi!",
                        showConfirmButton: !1,
                    }),
                        $('#m_modal_4').modal('hide');

                    function reload() {
                        location.reload();
                    }
                    setTimeout(reload, 2000);
                } else {
                    swal({
                        type: "error",
                        title: "Başarısız!",
                        textAlign: "left",
                        html: "Aşağıda yer alan maddeleri kontrol edip, tekrar deneyiniz! <br /><br />" +
                            "- Firma adı boş geçilemez!<br />" +
                            "- Firma kodu boş geçilemez!<br />"+
                            "- E-Mail adresi kullanılmakta veya boş geçilemez!<br />" +
                            "- Firma kodu kullanılmaktadır!<br />",
                        showConfirmButton: true,
                        confirmButtonText: "<span><i class='fa fa-window-close'></i></span>",
                        confirmButtonClass: "btn btn-danger m-btn m-btn--air m-btn--icon"
                    });
                }
            },
            error: (function () {
                swal({
                    type: "error",
                    title: "HATA!",
                    text: "Bağlantı Kurulamadı!",
                    showConfirmButton: !1,
                }),
                    $('#m_modal_4').modal('hide');

                function reload() {
                    location.reload();
                }

                setTimeout(reload, 2000);

            })
        });
    };

}