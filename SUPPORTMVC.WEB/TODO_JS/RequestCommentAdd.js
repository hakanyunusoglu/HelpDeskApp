var reqID = -1;
var modalbody = "#m_modal_4_comment_body";

$(function () {

    $('#m_modal_4_comment').on('show.bs.modal',
        function (e) {
            var btn = $(e.relatedTarget);
            reqID = btn.data("requests-id");
            $(modalbody).load("/RequestList/ShowRequestCommentModal/" + reqID);
        });
});
function NewComment(btn, mode) {
    if (mode === 'AddComment') {
        var formData = new FormData();
        formData.append('comReq', $(reqID).val());
        formData.append('CommentText', $('#m_summernote_1').val());
        formData.append('comStat', $('#comStat :selected').val());
        formData.append('ReqStatus', $('#ReqStatus :selected').val());
        formData.append('CommentTotalTime', $('#CommentTotalTime').text());
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
                        showConfirmButton: !1,
                        timer: 2000
                    }),
                        function reload() {
                            location.reload(true);
                            tr.hide();
                        }

                } else {
                    swal({
                        type: "error",
                        title: "Başarısız!",
                        text: "Sorun bildiriminiz sırasında bir hata oluştu!",
                        showConfirmButton: !1,
                        timer: 3000
                    }),
                        function reload() {
                            location.reload(true);
                            tr.hide();
                        }
                    //$(modalbody).load("/RequestList/ShowRequestCommentModal/" + reqID);
                }
            },
            error: (function () {
                swal({
                    type: "error",
                    title: "HATA!",
                    text: "Bağlantı Kurulamadı!",
                    showConfirmButton: !1
                }),
                    $('#m_modal_4_comment').modal('hide');
                function reload() {
                        location.reload();
                    }

                setTimeout(reload, 2000);

            })
        });
    }
}