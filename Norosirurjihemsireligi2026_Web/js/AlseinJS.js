const showLoadingIcon = (Kume, Icon) => {
    if (Kume) {
        $(Kume).css('display', 'none');
    }
    else {
        $('.btn-x').css('display', 'none');
    }


    if (Icon) {
        $(Icon).css('display', 'inline-block');
    }
    else {
        $('.LoadingIcon').css('display', 'inline-block');
    }


}

const UyariBilgilendirme = (Baslik, Icerik, Sonuc) => {
    $(document).ready(function () {
        if (Sonuc === undefined) {
            $('#UyariHead').css('background-color', 'transparent');
            $('#UyariBaslik').css('color', '#000');
            $('#UyariKapatButon').css('display', 'none');
        }
        else {
            if (Sonuc) {
                $('#UyariHead').css('background-color', 'darkseagreen');
                $('#UyariBaslik').css('color', '#fff');
            }
            else {
                $('#UyariHead').css('background-color', '#f00');
                $('#UyariBaslik').css('color', '#fff');
            }
            $('#UyariKapatButon').css('display', 'inline-block');
        }

        $('#UyariBaslik').html(Baslik);
        $('#UyariIcerik').html(Icerik);
        $('#Uyari').modal('show');
    });
};

const toUpper = (Kume) => {
    var index = Kume.selectionStart;
    Kume.value = Kume.value.replace("ı", "I").replace("i", "İ").toUpperCase();
    Kume.selectionStart = index;
    Kume.selectionEnd = index;
}

const datePickerOption = {
    startDate: null,
    endDate: null,
    placeholder: null,
    keyboardNavigation: false,
    autoclose: true,
    language: "tr",
    format: "dd.mm.yyyy"
}

const setDatePicker = () => {
    $('.x-date').datepicker(datePickerOption);
    $('.x-date').prop('readonly', 'true');
    $('.x-date').prop('placeholder', datePickerOption.placeholder);
}