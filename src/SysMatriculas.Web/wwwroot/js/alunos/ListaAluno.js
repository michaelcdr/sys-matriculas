class ListaAluno {
    constructor() {
        this._tableEl = $("#tb-alunos");
        this._token = $('input[name=__RequestVerificationToken]');
        this._oTable = null;
        this._controller = "aluno";

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

    eventos() {

    }

    desativar(el) {
        let _self = this;
        modalManager.mostrarConfirmacao(
            "Deseja desativar o aluno ?",
            "Esta ação não pode ser desfeita.", function () {
                let dados = {
                    id: el.data("id"),
                    __RequestVerificationToken: _self.token.val()
                };
                $.post(_self._controller + '/Desativar/', dados, function (data) {
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

    ativar(el) {
        let _self = this;
        modalManager.mostrarConfirmacao(
            "Deseja ativar o aluno ?",
            "Esta ação não pode ser desfeita.", function () {
                let dados = {
                    id: el.data("id"),
                    __RequestVerificationToken: _self.token.val()
                };
                $.post(_self._controller + '/Ativar/', dados, function (data) {
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

    remover(el) {
        let _self = this;
        modalManager.mostrarConfirmacao(
            "Deseja remover o aluno ?",
            "Esta ação não pode ser desfeita.", function () {                
                let dados = {                    
                    id: el.data("id"),
                    __RequestVerificationToken: _self.token.val()
                };
                $.post(_self._controller + '/Deletar/', dados, function (data) {
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

                $('.btn-ativar').unbind('click');
                $('.btn-ativar').click(function () {
                    _self.ativar($(this));
                });

                $('.btn-desativar').unbind('click');
                $('.btn-desativar').click(function () {
                    _self.desativar($(this));
                });
            },
            "columns": [
                {
                    "data": "id", "width":"110px", "orderable": false, "render": function (colVal, b, dataObj) {
                        let editarUrl = `/${_self._controller}/editar/${dataObj.id}`;
                        let retorno = dataTablesAux.gerarBotao('delete', dataObj.id, 'Remover aluno', 'trash');
                        retorno += dataTablesAux.gerarLink('edit', dataObj.id, 'Editar aluno', editarUrl, 'edit');
                        
                        if (dataObj.ativo) {
                            retorno += dataTablesAux.gerarBotao('desativar', dataObj.id, 'Desativar aluno', 'check');
                        } else {
                            retorno += dataTablesAux.gerarBotao('ativar', dataObj.id, 'Ativar aluno', 'check');
                        }
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
