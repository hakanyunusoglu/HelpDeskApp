var reqID = -1;
var comID = -1;
var takeattendantid = 0;

    var modalbody = "#m_modal_4_body";
    var modalbody2 = "#m_modal_4_comment_body";
    var modalbody3 = "#m_modal_4_takeRequest_body";
    var modalbody4 = "#m_modal_4_comment_answer_body";
    $(function() {

        $('#m_modal_4').on('show.bs.modal',
            function (e) {
                var btn = $(e.relatedTarget);
                reqID = btn.data("request-id");
                $(modalbody).load("/RequestList/ShowRequestCloseModal/" + reqID);

                $(document).ready(function () {
                    $("#reqStatus").empty();
                    $.ajax({
                        type: 'POST',
                        url: '/RequestList/FilterRequestCloseStatus/' + reqID,
                        dataType: 'json',
                        data: {},
                        success: function (FilterRequestCloseStatus) {
                            $("#reqStatus").append('<option value="0" selected disabled>Seçiniz</option>');
                            $.each(FilterRequestCloseStatus,
                                function (i, selectedRequestStatus) {
                                    $("#reqStatus").append('<option value="' +
                                        selectedRequestStatus.Value +
                                        '">' +
                                        selectedRequestStatus.Text +
                                        '</option>');
                                });
                        },
                        error: function (ex) {
                            alert('Durum Bilgilerine Ulaşılamadı!' + ex);
                        }
                    });
                });
            });
        
});
function CloseRequest(btn, mode) {
        if (mode === 'requestClose') {
            var formData = new FormData();
            formData.append('reqPriority', $('#ComReqPrio :selected').val());
            formData.append('ReqStatus', $('#reqStatus :selected').val());
            formData.append('CommentTotalTime', $('#CloseMinute').val());
            formData.append('CommentText', $('#m_autosize_1_kapat').val());
            $.ajax({
                type: "POST",
                url: '/RequestList/Edit/' + reqID,
                dataType: 'json',
                data: formData,
                contentType: false,
                processData: false,
                success: function(d) {
                    if (d.result) {
                        swal({
                                type: "success",
                                title: "Başarılı!",
                                text: "Talep bilgileri güncellendi!",
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
                            text:
                                "Talep önceliğini veya Talep durumunu değiştirmediyseniz, açıklama alanı boş geçilemez!",
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
        };

}

$(function() {
    $('#m_modal_4_comment').on('show.bs.modal',
        function (e) {
            
            var btn = $(e.relatedTarget);
            reqID = btn.data("requests-id");
            $(modalbody2).load("/RequestList/ShowRequestCommentModal/" + reqID);
            $(document).ready(function () {
                    $.ajax({
                        type: 'POST',
                        url: '/RequestList/FilterCommentStatus/' + reqID,
                        dataType: 'json',
                        data: {},
                        success: function (FilterCommentStatus) {
                            $("#CommentStatus").empty();
                            $("#CommentStatus").append('<option value="0" selected disabled>Seçiniz</option>');
                            $.each(FilterCommentStatus,
                                function(i, selectedStatus) {
                                    $("#CommentStatus").append('<option value="' +
                                        selectedStatus.Value +
                                        '">' +
                                        selectedStatus.Text +
                                        '</option>');
                                });
                        },
                        error: function(ex) {
                            alert('Durum Bilgilerine Ulaşılamadı!' + ex);
                        }
            });
            });
            $(document).ready(function () {
                    $.ajax({
                        type: 'POST',
                        url: '/RequestList/FilterRequestStatus/' + reqID,
                        dataType: 'json',
                        data: {},
                        success: function (FilterRequestStatus) {
                            $("#ComReqStat").empty();
                            $("#ComReqStat").append('<option value="0" selected disabled>Seçiniz</option>');
                            $.each(FilterRequestStatus,
                                function(i, selectedReqStatus) {
                                    $("#ComReqStat").append('<option value="' +
                                        selectedReqStatus.Value +
                                        '">' +
                                        selectedReqStatus.Text +
                                        '</option>');
                                });
                        },
                        error: function(ex) {
                            alert('Durum Bilgilerine Ulaşılamadı!' + ex);
                        }
                    });
            });
            $(document).ready(function () {
                $.ajax({
                    type: 'POST',
                    url: '/RequestList/FilterRequestStatusOwner/' + reqID,
                    dataType: 'json',
                    data: {},
                    success: function (FilterRequestStatusOwner) {
                        $("#CommentStatusOwner").empty();
                        $("#CommentStatusOwner").append('<option value="0" selected disabled>Seçiniz</option>');
                        $.each(FilterRequestStatusOwner,
                            function (i, selectedReqStatusOwner) {
                                $("#CommentStatusOwner").append('<option value="' +
                                    selectedReqStatusOwner.Value +
                                    '">' +
                                    selectedReqStatusOwner.Text +
                                    '</option>');
                            });
                    },
                    error: function (ex) {
                        alert('Durum Bilgilerine Ulaşılamadı!' + ex);
                    }
                });
            });
        });
});

function NewComment(btn, mode) {
    if (mode === 'AddComment') {
        var files = $("#RequestCommentFile").get(0).files;
        var formData = new FormData();
        formData.append('CommentText', $('#CommentText').val());
        formData.append('comStat', $('#CommentStatus :selected').val());
        formData.append('comUser', $('#CommentStatusOwner :selected').val());
        for (var i = 0; i < files.length; i++) {
            formData.append("RequestCommentFile", files[i]);
        }
        $.ajax({
            type: "POST",
            url: '/RequestList/NewComment/' + reqID,
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false,
            success: function (d) {
                if (d.result) {
                    swal({
                        type: "success",
                        title: "Başarılı!",
                        text: "Sorun bildiriminiz başarıyla eklenmiştir!",
                        showConfirmButton: false
                    });
                    $('#m_modal_4_comment').modal('hide');
                        function reload() {
                            location.reload();
                        }
                        setTimeout(reload, 2000);
                } else {
                    swal({
                        type: "error",
                        title: "Başarısız!",
                        text: "Lütfen tüm alanların doldurulduğundan emin olunuz!",
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
                    text: "Bir sorun oluştu!",
                    showConfirmButton: false,
                    confirmButtonText: "<span><i class='fa fa-window-close'></i></span>",
                    confirmButtonClass: "btn btn-danger m-btn m-btn--air m-btn--icon"
                });
                    function reload() {
                        location.reload();
                    }
                setTimeout(reload, 2000);
            })
        });
    }
};

$(function () {
    $('#m_modal_4_takerequest').on('show.bs.modal',
        function (e) {
            var btn = $(e.relatedTarget);
            reqID = btn.data("request-take-id");
            $(modalbody3).load("/RequestList/ShowTakeRequestModal/" + reqID);

        });

});
$(document).ready(function () {
    $('#CommentForm input').on('change', function () {
        var x = $('input[name=example_3]:checked', '#CommentForm').val();

        if (x < 1) {
            $('#CommentBodyField').attr("style", "visibility:hidden");
        } else {
            $('#CommentBodyField').attr("style", "visibility:visible");
        }
        if (x == 1) {
            $('#DivCommentStatusValueUsers').show();
            $('#DivCommentStatusValueUser').show();
            $('#DivCommentStatusValueHbs').hide();
            $('#DivCommentTotalTime').hide();
            $('#DivCommentPriorityValue').hide();
        }
        if (x == 2) {
            $('#DivCommentStatusValueUsers').hide();
            $('#DivCommentStatusValueUser').hide();
            $('#DivCommentStatusValueHbs').show();
            $('#DivCommentTotalTime').show();
            $('#DivCommentPriorityValue').show();
        }
    });
});
function TakeRequestAttendant(btn, mode) {
    if (mode === 'TakerequestAttendant') {
        var y = $('input[name=example_3]:checked', '#TakeRequestAttendantForm').val();

        var formData = new FormData();
        if (y == 1) {
            formData.append('TakeRequestSelectedValue', $('#RequestAttendantMe :selected').val());
        }
        if (y == 2) {
            formData.append('TakeRequestSelectedValue', $('#RequestAttendantOther :selected').val());
        }
        
        $.ajax({
            type: "POST",
            url: '/Request/TakeRequestAttendantUser/' + reqID,
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false,
            success: function (d) {
                if (d.result) {
                    swal({
                        type: "success",
                        title: "Başarılı!",
                        text: "Görevlendirme başarıyla tamamlandı!",
                        showConfirmButton: !1,
                    }),
                        $('#m_modal_4_takerequest').modal('hide');

                    function reload() {
                        location.reload();
                    }
                    setTimeout(reload, 2000);
                } else {
                    swal({
                        type: "error",
                        title: "Başarısız!",
                        text: "Lütfen görevlendirilecek kişiyi seçiniz!",
                        showConfirmButton: !0,
                        confirmButtonText: "<span><i class='fa fa-window-close'></i></span>",
                        confirmButtonClass: "btn btn-danger m-btn m-btn--air m-btn--icon"
                    }),
                        $(modalbody3).load("/RequestList/ShowTakeRequestModal/" + reqID);
                }
            },
            error: (function () {
                swal({
                    type: "error",
                    title: "HATA!",
                    text: "Bağlantı Kurulamadı!",
                    showConfirmButton: !1,
                }),
                    $('#m_modal_4_takerequest').modal('hide');

                function reload() {
                    location.reload();
                }

                setTimeout(reload, 2000);

            })
        });
    };

}

$(function () {
    $('#m_modal_4_comment_answer').on('show.bs.modal',
        function (e) {
            var btn = $(e.relatedTarget);
            comID = btn.data("comment-id");
            $(modalbody4).load("/RequestList/ShowAnswerModal/" + comID);
        });

});
function CommentAnswer(btn, mode) {
    if (mode === 'CommentAnswer') {
        var formData = new FormData();
        //formData.append('CommentText', $('#m_summernote_1').summernote('code'));
        formData.append('CommentText', $('#m_autosize_1_kapat').val());
        $.ajax({
            type: "POST",
            url: '/RequestList/NewCommentAnswer/' + comID,
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false,
            success: function (d) {
                if (d.result) {
                    swal({
                        type: "success",
                        title: "Başarılı!",
                        text: "Yorumunuz başarıyla eklenmiştir!",
                        showConfirmButton: !1
                    }),
                        $('#m_modal_4_comment_answer').modal('hide');

                    function reload() {
                        location.reload();
                    }
                    setTimeout(reload, 2000);
                } else {
                    swal({
                        type: "error",
                        title: "Başarısız!",
                        text: "Yorum eklenirken bir hata oluştu!",
                        showConfirmButton: !1,
                    }),
                        $(modalbody4).load("/RequestList/ShowAnswerModal/" + comID);
                }
            },
            error: (function () {
                swal({
                    type: "error",
                    title: "HATA!",
                    text: "Bağlantı Kurulamadı!",
                    showConfirmButton: !1,
                }),
                    $('#m_modal_4_comment_answer').modal('hide');

                function reload() {
                    location.reload();
                }

                setTimeout(reload, 2000);

            })
        });
    };

}