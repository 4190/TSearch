$(document).ready(function () {
    $('#target').hide();
    $('#target').delay(100).load('/Account/GetPartial/_ProfileGeneralPartial/' + $('#profile').attr('data-user-id')).fadeIn();
});

$('#general').click(function () {
    $('#target').fadeOut(50).load('/Account/GetPartial/_ProfileGeneralPartial/' + $('#profile').attr('data-user-id')).fadeIn();
    if (!($('#general').hasClass('active'))) {
        $('#general').addClass('active');
    }
    if ($('#change-password').hasClass('active')) {
        $('#change-password').removeClass('active');
    }
    if ($('#set-country').hasClass('active')) {
        $('#set-country').removeClass('active');
    }

});

$('#change-password').click(function () {
    $('#target').fadeOut(50).load('/Account/GetPartial/_ProfileChangePasswordPartial/' + $('#profile').attr('data-user-id')).fadeIn();
    if ($('#general').hasClass('active')) {
        $('#general').removeClass('active');
    }
    if (!($('#change-password').hasClass('active'))) {
        $('#change-password').addClass('active');
    }
    if ($('#set-country').hasClass('active')) {
        $('#set-country').removeClass('active');
    }  
});

$('#set-country').click(function () {
    $('#target').fadeOut(50).load('/Account/GetPartial/_ProfileSetCountryPartial/' + $('#profile').attr('data-user-id')).fadeIn();
    if (!($('#set-country').hasClass('active'))) {
        $('#set-country').addClass('active');
    }
    if ($('#general').hasClass('active')) {
        $('#general').removeClass('active');
    }
    if ($('#change-password').hasClass('active')) {
        $('#change-password').removeClass('active');
    }
});
