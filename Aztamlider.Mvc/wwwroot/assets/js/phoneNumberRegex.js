$(document).ready(function () {

    $('#contactNumberInput').on('blur', function () {
        var phoneNumber = $(this).val();
        var numberRegex = /^(050|051|055|070|077|010|099)(\d{7})$/;

        if (!numberRegex.test(phoneNumber)) {
            $('#numberError').text('Zəhmət olmasa düzgün telefon nömrəsi daxil edin.');
            $('#numberError').css('padding', '3px 15px');

        } else {
            $('#numberError').text('');
            $('#numberError').css('padding', '0px');

        }
    });
});
