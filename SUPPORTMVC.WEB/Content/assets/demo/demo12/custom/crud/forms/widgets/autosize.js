var Autosize = {
    init: function () {
        var i, t, o,z;
        i = $("#m_autosize_1_reqdesc"), t = $("#m_autosize_1"), o = $("#m_autosize_1_kapat"), z = $("#CommentText"),
            autosize(i), autosize(t), autosize(o), autosize(z), autosize.update(t);
    }
};
jQuery(document).ready(function () {
    Autosize.init()
});