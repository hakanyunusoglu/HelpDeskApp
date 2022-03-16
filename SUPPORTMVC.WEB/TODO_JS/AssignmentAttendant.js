var id = -1;
var cancelid = -1;
$(".confirmid").click(function () {
    id = $(this).attr("data-confirmid");
});
$(".cancelid").click(function () {
    cancelid = $(this).attr("data-cancelid");
});

function AssignmentAttendantConfirm(btn, mode) {    
    if (mode === 'AssignmentAttendantConfirm') { 
        $(document).ready(function () {
            
            $.ajax({
                type: "POST",
                url: '/QuestList/AssignmentAttendant/' + id,
                dataType: 'json',
                data: {},
                success: function(d) {
                    if (d.result) {
                        swal({
                                type: "success",
                                title: "Başarılı!",
                                text: "Görev ataması başarıyla tamamlandı!",
                                showConfirmButton: !1,
                        }),
                            $('#m_modal_4_comment').modal('hide');
                            function reload() {
                                location.reload();
                            }
                        setTimeout(reload, 2000);
                    } else {
                        swal({
                            type: "error",
                            title: "Başarısız!",
                            text: "Görev ataması sırasında bir hata oluştu!",
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

                    function reload() {
                        location.reload();
                    }

                    setTimeout(reload, 2000);

                })
            });
        });
    }
};
function AssignmentAttendantCancel(btn, mode) {
    if (mode === 'AssignmentAttendantCancel') {
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: '/QuestList/AssignmentAttendantCancel/' + cancelid,
                dataType: 'json',
                data: {},
                success: function (d) {
                    if (d.result) {
                        swal({
                                type: "success",
                                title: "Başarılı!",
                                text: "Görev atamasını iptal ettiniz!",
                                showConfirmButton: !1,
                            }),
                            $('#m_modal_4_comment').modal('hide');
                        function reload() {
                            location.reload();
                        }
                        setTimeout(reload, 2000);
                    } else {
                        swal({
                            type: "error",
                            title: "Başarısız!",
                            text: "Görev atamasının iptali sırasında bir hata oluştu!",
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
        });
    }
};