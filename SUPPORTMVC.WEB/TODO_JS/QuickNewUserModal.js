var modalbody = "#m_modal_4_body";

$(function () {

    $('#m_modal_4').on('show.bs.modal',
        function (e) {
            var btn = $(e.relatedTarget);
            $(modalbody).load("/Request/ShowQuickNewUserModal/");
        });
});

function QuickNewUser(btn, mode) {
    if (mode === 'quickuser') {
        var formData = new FormData();
        formData.append('Username', $('#Username').val());
        formData.append('Password', $('#Password').val());
        formData.append('UName', $('#UName').val());
        formData.append('USurname', $('#USurname').val());
        formData.append('Email', $('#Email').val());
        formData.append('CompanyID', $('#m_select2_2 :selected').val());
        formData.append('RoleID', $('#RoleID').val());
        formData.append('IsActive', $('#IsActive').is(':checked'));
        $.ajax({
            type: "POST",
            url: '/User/QuickReqUser',
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false,
            success: function(d) {
                if (d.result) {
                    swal({
                            type: "success",
                            title: "Başarılı!",
                            text: "Kullanıcı kaydı başarıyla tamamlanmıştır!",
                        showConfirmButton: !1,
                            timer: 1000
                        }),
                        $('#m_modal_4').modal('hide');
                } else {
                    swal({
                        type: "error",
                        title: "Başarısız!",
                        textAlign: "left",
                        html: "Aşağıda yer alan maddeleri kontrol edip, tekrar deneyiniz! <br /><br />" +
                            "- Kullanıcı adı kullanılmaktadır!<br />" +
                            "- E-Mail adresi kullanılmakta veya boş geçilemez!<br />" +
                            "- Geçersiz rol girilmiştir!<br />" +
                            "- Ad ve Soyad alanları boş geçilemez!",
                        showConfirmButton: true,
                        confirmButtonText: "<span><i class='fa fa-window-close'></i></span>",
                        confirmButtonClass: "btn btn-danger m-btn m-btn--air m-btn--icon"
                    });
                }
            },
            error: (function() {
                swal({
                        type: "error",
                        title: "HATA!",
                        text: "Bağlantı Kurulamadı!",
                        showConfirmButton: !1,
                    }),
                    $('#m_modal_4').modal('hide');

            })
        });
    };
}