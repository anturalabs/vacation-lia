/*
                    $(function () {
                        $('.editModal').modal();
                    });
                    */
function editAbsence(absenceId) {
    $.ajax({
        url: '/Home/EditAbsence/' + absenceId, // The method name + paramater
        success: function (data) {
            $('#SemesterModal').html(data); // This should be an empty div where you can inject your new html (the partial view)
        }
    });
}


$(function () {
    $("td.editAbsence").click(function () {
        var getIdFromCell = $(event.target).closest('td').data('id'); //get the id from td
        
        $.ajax({
            url: '/Home/EditAbsence/' + getIdFromCell, // The method name + parameter
            success: function (data) {
                // $('#selectedAbsence').value()

                $('#selectedUserButton').prop('disabled', true);
                $('#DeleteButton').removeClass('hidden');
                $('#selectedApprovalButton').removeClass('hidden');
                $('#SubmitButton').val('Save');

                var userName = $('#user-select').find('.small[data-value="' + data[0].usersID + '"]').html().trim();
                $('#comments').val(JSON.stringify(data[0]));
                $('#CommentField').val(data[0].commentField);
                //' + data[0].usersID + '
                $('#selectedUserButton').html(userName + ' <span class="caret"></span>');
                $('#selectedUser').val(data[0].usersID);
                $('#guidId').val(data[0].id);

                $("#approval-select li a").click(function () {
                    document.getElementById('selectedApproval').value = document.getElementById('selectedApprovalButton').value;
                });

                $('#SemesterView').find('input[name="FromDate"]').datepicker('update', new Date(data[0].date));
                $('#SemesterView').find('input[name="ToDate"]').datepicker('update', new Date(data[data.length - 1].date).toISOString().slice(0, 10));
                $('#selectedAbsenceButton').html(data[0].absenceName + ' <span class="caret"></span>');
                $('#selectedAbsence').val(data[0].absenceName);
                $('#selectedApprovalButton').html(data[0].approval + ' <span class="caret"></span>');
                $('#selectedApproval').val(data[0].approval);
            }
        });
        $('#SemesterModal').modal();
        //  $('#Semesterbody').html(getIdFromCell)
    });
});
    /*
            $(function () {
                $('#SemesterModal').modal({
                    keyboard: true,
                    backdrop: "static",
                    show: false,

                }).on('show', function () { //subscribe to show method
                    var getIdFromCell = $(event.target).closest('td').data('id'); //get the id from tr
                    alert(getIdFromCell);
                    //make your ajax call populate items or what even you need
                    $(this).find('#SemesterBody').html($(getIdFromCell))
                });
            }); */