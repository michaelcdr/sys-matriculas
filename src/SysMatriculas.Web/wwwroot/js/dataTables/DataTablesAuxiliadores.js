class DataTablesAuxiliadores  {
    constructor() {

    }

    obterLanguage() {
        return {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ at&eacute; _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 at&eacute; 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ resultados por p&aacute;gina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        };
    }

    gerarLink(type, id, title, url, icone) {
        return "<a class='btn btn-primary btn-" + type + " btn-sm' " +
            "id='btn-" + type + "-" + id + "' href='"+url+"' " +
            "data-toggle='tooltip' " +
            "data-placement='top' title='" + title + "' " +
            "data-container='body'  data-id='" + id + "' >" +
            "     <i class='fa fa-" + icone + "'></i>" +
            "</a> ";
    }

    gerarBotao(type, id, title, icone) {
        let tipoDeBtn = "primary";
        if (type === 'delete')
            tipoDeBtn = "danger";
        else if (type === 'desativar')
            tipoDeBtn = "success";
        else if (type === 'ativar')
            tipoDeBtn = "danger";

        return "<button class='btn btn-" + tipoDeBtn + " btn-" + type + " btn-sm' " +
            "id='btn-" + type + "-" + id + "' type='button' " +
            "data-toggle='tooltip' " +
            "data-placement='top' title='" + title + "' " +
            "data-container='body'  data-id='" + id + "' " +
            "data-loading-text='Carregando <img src=" + loaderGif + " />'>" +
            "     <i class='fa fa-" + icone + "'></i>" +
            "</button> ";
    }
}