    var reqID = -1;

    $(function () {

        $('#m_modal_4_comment').on('show.bs.modal',
            function (e) {
                var btn = $(e.relatedTarget);
                reqID = btn.data("requests-id");
                $(modalbody2).load("/RequestList/ShowRequestCommentModal/" + reqID);

                $("#CommentStatus").empty();
                $.ajax({
                    type: 'POST',
                    url: '/RequestList/FilterCommentStatus/' + reqID,
                    dataType: 'json',
                    data: {},
                    success: function (filterAttendant) {
                        $("#CommentStatus").append('<option value="0">Seçiniz</option>');
                        $.each(filterAttendant,
                            function (i, selectedUser) {
                                $("#CommentStatus").append('<option value="' + selectedUser.Value + '">' + selectedUser.Text + '</option>');
                            });
                    },
                    error: function (ex) {
                        alert('Durum Bilgilerine Ulaşılamadı!' + ex);
                    }
                });
                return false;
            });
    });