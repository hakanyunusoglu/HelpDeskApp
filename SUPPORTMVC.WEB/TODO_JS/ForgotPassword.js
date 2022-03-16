function ForgotPasswordFunc(btn, mode) {
    if (mode === 'forgotPassword') {
        var formData = new FormData();
        formData.append('Email', $('#txtsifremiunuttummail').val());
        $.ajax({
            type: "POST",
            url: '/Login/ForgotPassword',
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false,
            success: function (d) {
                if (d.result) {
                    swal({
                        type: "success",
                        title: "Başarılı!",
                        text: "Kullanıcı bilgileriniz e-mail adresinize gönderilmiştir!",
                        showConfirmButton: !1
                    }),
                        $("#txtsifremiunuttummail").empty();

                        function reload() {
                            location.reload();
                        }
                    setTimeout(reload, 3000);
                } else {
                    swal({
                        type: "error",
                        title: "E-mail adresi bulunamadı!",
                        text: "Lütfen girmiş olduğunuz e-mail adresini kontrol ederek doğruluğundan emin olun!",
                        showConfirmButton: !0,
                        confirmButtonText: "<span><i class='fa fa-window-close'></i></span>",
                        confirmButtonClass: "btn btn-danger m-btn m-btn--air m-btn--icon"
                    }),
                        $("#txtsifremiunuttummail").empty();

                }
            },
            error: (function () {
                swal({
                    type: "error",
                    title: "HATA!",
                    text: "Geçersiz e-mail adresi!",
                    showConfirmButton: !0,
                    confirmButtonText: "<span><i class='fa fa-window-close'></i><span>Kapat</span></span>",
                    confirmButtonClass: "btn btn-danger m-btn m-btn--pill m-btn--air m-btn--icon"
                }),
                    $("#txtsifremiunuttummail").empty();
            })
        });
    };

}