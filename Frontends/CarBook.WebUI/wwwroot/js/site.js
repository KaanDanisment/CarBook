$(function () {

    $('#pickup-location,#dropoff-location').autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: "/Rental/GetLocations",
                type: "GET",
                data: { query: request.term },
                success: function (data) {
                    response(data);
                },
                error: function (xhr, status, error) {
                    console.error("Autocomplete error:", status, error);
                }
            });
        },
        select: function (event, ui) {
            // Seçilen input'un ID'sini al
            var selectedInputId = $(this).attr('id');

            // Seçilen input'a label (isim) yaz
            $(this).val(ui.item.label);

            // Hidden input ID’sini oluştur
            var hiddenInputId = '#' + selectedInputId + '-id';

            // Hidden input'a ID değerini yaz
            $(hiddenInputId).val(ui.item.value);

            return false; // input'a value (ID) yazılmasını engelle
        }
    });


    $('#rent-date,#return-date').datepicker({
        'format': 'm/d/yyyy',
        'autoclose': true,
        startDate: new Date(),
    });
    $('#rent-date').on('changeDate', function (e) {
        const selectedRentDate = e.date;
        $('#return-date').datepicker('setStartDate', selectedRentDate);
    });
    $('#return-date').on('changeDate', function (e) {
        const selectedReturnDate = e.date;
        $('#rent-date').datepicker('setEndDate', selectedReturnDate);
    });

    $('#rent-time, #return-time').timepicker({
        minTime: '08:00am',
        maxTime: '8:00pm',
        showMeridian: false
    });



    $('#rental-form').on('submit', function (e) {
        let isValid = true;

        $('#pickupError').text('');
        $('#dropoffError').text('');

        const pickupId = $('#pickup-location-id').val();
        const dropoffId = $('#dropoff-location-id').val();

        if (!pickupId) {
            $('#pickupError').text('Lütfen kiralama lokasyonu seçiniz.');
            isValid = false;
        } else if (pickupId == 0) {
            $('#pickupError').text('Konum bilgisi olmadan işlem yapılamaz.');
            isValid = false;
        }

        if (!dropoffId) {
            $('#dropoffError').text('Lütfen teslimat lokasyonu seçiniz.');
            isValid = false;
        } else if (dropoffId == 0) {
            $('#dropoffError').text('Konum bilgisi olmadan işlem yapılamaz.');
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault();
        }
    });
});