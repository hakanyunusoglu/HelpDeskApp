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
    $('#TakeRequestAttendantForm input').on('change', function () {
        var y = $('input[name=example_3]:checked', '#TakeRequestAttendantForm').val();
        
        if (y <= 1) {
            $('#DivRequestAttendantMe').show();
            $('#DivRequestAttendantOther').attr("style", "visibility:hidden");
        } 
        if (y == 2) {
            $('#DivRequestAttendantOther').attr("style", "visibility:visible");
            $('#DivRequestAttendantMe').hide();
            
        }
    });
});