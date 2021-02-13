

$('#check').click(function () {
    $('#check-result').fadeOut(50).load('/Account/VerifyCheck/' + $('#check').attr('character-name-js') + '/' + $('#check').attr('verification-token-js')).fadeIn();

});
