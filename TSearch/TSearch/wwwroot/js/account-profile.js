$(document).ready(function () {
    $('#target').load('/Account/GetPartial/_ProfileGeneralPartial/' + $('#profile').attr('data-user-id'));
});

$('#general').click(function () {
    $('#target').load('/Account/GetPartial/_ProfileGeneralPartial/' + $('#profile').attr('data-user-id'));
    if (!($('#general').hasClass('active'))) {
        $('#general').addClass('active');
    }
    if ($('#change-password').hasClass('active')) {
        $('#change-password').removeClass('active');
    }

});

$('#change-password').click(function () {
    $('#target').load('/Account/GetPartial/_ProfileChangePasswordPartial/' + $('#profile').attr('data-user-id'));

    if ($('#general').hasClass('active')) {
        $('#general').removeClass('active');
    }
    if (!($('#change-password').hasClass('active'))) {
        $('#change-password').addClass('active');
    }
});
