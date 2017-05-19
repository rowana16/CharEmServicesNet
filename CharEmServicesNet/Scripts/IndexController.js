$('#submitLocation').on('click', function (e) {
    
    var selectedCounty = $('#selectedCounty option:selected').val();        
    var selectedCity = $('#selectedCity option:selected').val();   
    var selectedSchool = $('#selectedSchool option:selected').val();
    
    
    $.ajax({
        url: "/Home/LocationPartial",
        type: "POST",
        data: {
            'selectedCounty': selectedCounty,
            'selectedCity': selectedCity,
            'selectedSchool': selectedSchool
        }
    })
    .done(function (partialViewResult) {
        $('#locationPartial').html(partialViewResult);
    });
});