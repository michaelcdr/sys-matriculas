class ListaUsuario {
    constructor() {
        this._tableEl = $("#tb-usuarios");
        this._token = $('input[name=__RequestVerificationToken]');
        this._oTable = null;
        this._controller = "usuario";

        this.construirDataTables();
    }

    get oTable() {
        return this._oTable;
    }

    get tableEl() {
        return this._tableEl;
    }

    get token() {
        return this._token;
    }

    remover(el) {
        let _self = this;
        modalManager.mostrarConfirmacao(
            "Deseja remover o usuario ?",
            "Esta ação não pode ser desfeita.", function () {
                let dados = {
                    id: el.data("id"),
                    __RequestVerificationToken: _self.token.val()
                };
                $.post(_self._controller + '/Remover/', dados, function (data) {

                    if (data.sucesso) {
                        _self._oTable.fnDraw();
                        swal.close();
                    } else {
                        modalManager.mostrarErro(data.titulo, data.mensagem);
                    }
                });
            }
        );
    }

    construirDataTables() {
        let _self = this;
        
        _self._oTable = _self._tableEl.dataTable({
            "iDisplayLength": 100,
            "sPaginationType": "full_numbers",
            "processing": true,
            "serverSide": true,
            "language": dataTablesAux.obterLanguage(),
            "ajax": {
                "url": `/${_self._controller}/Listar`,
                "type": "POST",
                "data": function (d) {
                }
            },
            'createdRow': function (row, data, dataIndex) {
                
            },
            "fnDrawCallback": function () {
                $('.btn-delete').unbind('click');
                $('.btn-delete').click(function () {
                    _self.remover($(this));
                });
            },
            "columns": [
                {
                    "data": "id", "width":"60px", "orderable": false, "render": function (colVal, b, dataObj) {
                        let editarUrl = `/${_self._controller}/Editar/${dataObj.id}`;
                        let retorno = dataTablesAux.gerarBotao('delete', dataObj.id, 'Remover usuário','trash');
                        retorno += dataTablesAux.gerarLink('edit', dataObj.id, 'Editar usuário', editarUrl,'edit');
                        return retorno;
                    }
                },
                { "data": "nome" },
                { "data": "sobreNome" },
                { "data": "email" },
                { "data": "userName" }
            ]
        });
        _self.oTable.fnAdjustColumnSizing();
    }
}
