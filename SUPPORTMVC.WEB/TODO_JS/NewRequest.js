function NewRequestAdd(btn, mode) {
    if (mode === 'AddNewRequest') {
        var files = $("#NewRequestFiles").get(0).files;
        var formData = new FormData();

        formData.append('ReqStatus', $('#reqStatus :selected').val());
        formData.append('CommentTotalTime', $('#CommentTotalTime').val());
        formData.append('CommentText', $('#m_autosize_1_reqdesc').val());
        formData.append('RequestDate', $('#m_datepicker_3').val());
        formData.append('ReqCompany', $('#m_select2_1 :selected').val());
        formData.append('ReqUser', $('#m_form_TalepSahibi :selected').val());
        formData.append('reqPriority', $('#txttaleponcelik :selected').val());
        formData.append('RequestType', $('#RequestType :selected').val());
        formData.append('RequestDescription', $('#m_autosize_1').val());
        formData.append('UserDatetime', $('#UserDatetime').val());
        for (var i = 0; i < files.length; i++) {
            formData.append('NewRequestFiles', files[i]);
        }
        $.ajax({
            type: "POST",
            url: '/Request/RequestReg',
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false,
            success: function (d) {
                if (d.result) {
                    swal({
                        type: "success",
                        title: "Başarılı!",
                        text: "Talep kaydınız oluşturulmuştur!",
                        showConfirmButton: false
                    });
                        function reload() {
                            location.reload();
                        }
                    setTimeout(reload, 2000);  
                } else {
                    swal({
                        type: "error",
                        title: "Başarısız!",
                        text: "Talep oluşturulamadı. Tüm alanların eksiksiz doldurulduğundan emin olunuz!",
                        showConfirmButton: true,
                        confirmButtonText: "<span><i class='fa fa-window-close'></i></span>",
                        confirmButtonClass: "btn btn-danger m-btn m-btn--air m-btn--icon"
                    });}
            },
            error: (function () {
                swal({
                    type: "error",
                    title: "Başarısız!",
                    text: "Talep oluşturulamadı. Tüm alanların eksiksiz doldurulduğundan emin olunuz!",
                    showConfirmButton: !0,
                    confirmButtonText: "<span><i class='fa fa-window-close'></i></span>",
                    confirmButtonClass: "btn btn-danger m-btn m-btn--air m-btn--icon"
                });
            })
        });
    }
};
function NewRequestAddReproduce(btn, mode) {
    if (mode === 'AddNewRequestReproduce') {
        var files = $("#NewRequestFiles").get(0).files;
        var formData = new FormData();

        formData.append('ReqStatus', $('#reqStatus :selected').val());
        formData.append('CommentTotalTime', $('#CommentTotalTime').val());
        formData.append('CommentText', $('#m_autosize_1_reqdesc').val());
        formData.append('RequestDate', $('#m_datepicker_3').val());
        formData.append('ReqCompany', $('#m_select2_1 :selected').val());
        formData.append('ReqUser', $('#m_form_TalepSahibi :selected').val());
        formData.append('reqPriority', $('#txttaleponcelik :selected').val());
        formData.append('RequestType', $('#RequestType :selected').val());
        formData.append('RequestDescription', $('#m_autosize_1').val());
        formData.append('UserDatetime', $('#UserDatetime').val());
        for (var i = 0; i < files.length; i++) {
            formData.append('NewRequestFiles', files[i]);
        }
        $.ajax({
            type: "POST",
            url: '/Request/RequestReg',
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false,
            success: function (d) {
                if (d.result) {
                    swal({
                        type: "success",
                        title: "Başarılı!",
                        text: "Talep kaydınız oluşturulmuştur!",
                        showConfirmButton: false,
                        timer: 3500,
                    });
                    document.getElementById("m_autosize_1").value = "";
                } else {
                    swal({
                        type: "error",
                        title: "Başarısız!",
                        text: "Talep oluşturulamadı. Tüm alanların eksiksiz doldurulduğundan emin olunuz!",
                        showConfirmButton: true,
                        confirmButtonText: "<span><i class='fa fa-window-close'></i></span>",
                        confirmButtonClass: "btn btn-danger m-btn m-btn--air m-btn--icon"
                    });
                }
            },
            error: (function () {
                swal({
                    type: "error",
                    title: "Başarısız!",
                    text: "Talep oluşturulamadı. Tüm alanların eksiksiz doldurulduğundan emin olunuz!",
                    showConfirmButton: !0,
                    confirmButtonText: "<span><i class='fa fa-window-close'></i></span>",
                    confirmButtonClass: "btn btn-danger m-btn m-btn--air m-btn--icon"
                });
            })
        });
    }
};