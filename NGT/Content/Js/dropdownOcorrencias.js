$(document).ready(function () {
    $('#Bloco').change(function () {
        $.ajax({
            url: 'ocorrencias/carregaLocal',
            type: 'POST',
            data: { blocoId: $(this).val() },
            datatype: 'json',
            success: function (data) {
                var options = '';
                options += '<option value>Selecione o Local</option>';
                $.each(data, function () {
                    options += '<option value="' + this.id + '">' + this.nome + '</option>';
                });
                $('#Local').html(options);
                $('#divLocais').removeClass('d-none');
                $('#divCategs').removeClass('d-none');
            },
        });
    });
});

$(document).ready(function () {
    
    $('#Categoria').change(function () {
        if ($('#Categoria').val() || $('#Local').val()!==""){
            $.ajax({
                url: 'ocorrencias/carregaitem',
                type: 'POST',
                data: {
                    localId: $('#Local').val(),
                    categId: $('#Categoria').val()
                },
                datatype: 'json',
                success: function (data) {
                    var options = '';
                    options += '<option>Selecione o Item</option>';
                    $.each(data, function () {
                        options += '<option value="' + this.id + '">' + this.nome + '</option>';
                    });
                    $('#Item').html(options);
                    $('#divItens').removeClass('d-none');
                    $('#divMotivos').removeClass('d-none');
                    $('#divObs').removeClass('d-none');
                }
            });
        }
    });

    $('#Local').change(function () {
        if ($('#Categoria').val() || $('#Local').val() !== "") {
            $.ajax({
                url: 'ocorrencias/carregaitem',
                type: 'POST',
                data: {
                    localId: $('#Local').val(),
                    categId: $('#Categoria').val()
                },
                datatype: 'json',
                success: function (data) {
                    var options = '';
                    options += '<option>Selecione o Item</option>';
                    $.each(data, function () {
                        options += '<option value="' + this.id + '">' + this.nome + '</option>';
                    });
                    $('#Item').html(options);
                    $('#divItens').removeClass('d-none');
                    $('#divMotivos').removeClass('d-none');
                    $('#divObs').removeClass('d-none');
                }
            });
        }
    });

});     

$(document).ready(function () {
    $('#Item').change(function () {
        $('#divSubmit').removeClass('d-none');
    });
});