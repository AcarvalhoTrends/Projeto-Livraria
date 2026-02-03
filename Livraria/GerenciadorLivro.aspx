<%@ Page Title="Gerenciador de Livros" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GerenciadorLivro.aspx.cs" Inherits="ProjetoLivrariaTrends.Livraria.GerenciadorLivro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnEndCallback(s, e) {
            if (s.cpRedirectToLivros) {
                window.location.href = '/Livraria/GerenciadorLivro.aspx';
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding: 20px;">
        
        <%-- Bloco de Cadastro Original --%>
        <dx:ASPxFormLayout ID="ASPxFormLayoutPrincipal" runat="server" Width="50%" Theme="Office365">
            <Items>
                <dx:LayoutGroup Caption="Cadastro de Livro" ColCount="2" SettingsItemCaptions-Location="Top">
                    <Items>
                        <%-- Campo Categoria --%>
                        <dx:LayoutItem Caption="Categoria">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox ID="cmbCadastroTipoLivro" runat="server" Width="100%" ValueField="LIV_ID_LIVRO" TextField="TIL_DS_DESCRICAO" ValueType="System.Decimal">
                                        <ValidationSettings ValidationGroup="MyGroup" Display="Dynamic">
                                            <RequiredField IsRequired="True" ErrorText="Selecione a categoria!" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <%-- Campo Editor --%>
                        <dx:LayoutItem Caption="Editor">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox ID="cmbCadastroIdEditorLivro" runat="server" Width="100%" ValueField="LIV_ID_TIPO_LIVRO" TextField="EDI_NM_EDITOR" ValueType="System.Decimal">
                                        <ValidationSettings ValidationGroup="MyGroup" Display="Dynamic">
                                            <RequiredField IsRequired="True" ErrorText="Selecione o editor!" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <%-- Campo Autor --%>
                        <dx:LayoutItem Caption="Autor">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox ID="cmbCadastroAutor" runat="server" Width="100%" ValueField="AUT_ID_AUTOR" TextField="AUT_NM_NOME" ValueType="System.Decimal">
                                        <ValidationSettings ValidationGroup="MyGroup" Display="Dynamic">
                                            <RequiredField IsRequired="True" ErrorText="Selecione o autor!" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <%-- Campo Titulo --%>
                        <dx:LayoutItem Caption="Título">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="tbxCadastroTituloLivro" runat="server" Width="100%">
                                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                            <RequiredField IsRequired="True" ErrorText="Digite o título do Livro!" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <%-- Campo Preco --%>
                        <dx:LayoutItem Caption="Preço">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="tbxCadastroPrecoLivro" runat="server" Width="100%">
                                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                            <RequiredField IsRequired="true" ErrorText="Informe o preço do Livro!" />
                                            <dx:RegularExpression ErrorText="Preço inválido (ex: 29,90)" ValidationExpression="^\d+([,.]\d{1,2})?$" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <%-- Campo Royalty --%>
                        <dx:LayoutItem Caption="Royalty (%)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="tbxCadastroRoyaltyLivro" runat="server" Width="100%">
                                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                            <RequiredField IsRequired="True" ErrorText="Informe o royalty do Livro!" />
                                            <dx:RegularExpression ErrorText="Royalty inválido (0 a 100)" ValidationExpression="^100([,.]0{1,2})?$|^\d{1,2}([,.]\d{1,2})?$" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <%-- Campo Edicao Livro --%>
                        <dx:LayoutItem Caption="Edição Livro">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="tbxCadastroEdicaoLivro" runat="server" Width="100%">
                                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                            <RequiredField IsRequired="true" ErrorText="Informe a edição do Livro!" />
                                            <dx:RegularExpression ErrorText="Edição inválida (números inteiros)" ValidationExpression="^\d+$" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <%-- Campo Resumo --%>
                        <dx:LayoutItem Caption="Resumo" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxTextBox ID="tbxCadastroResumoLivro" runat="server" Width="100%" TextMode="MultiLine" Rows="5">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <%-- Botão Salvar --%>
                        <dx:LayoutItem Caption="" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnSalvarLivro" runat="server" Text="Salvar" AutoPostBack="true" Width="100%" OnClick="BtnNovoAutor_Click" ValidationGroup="MyGroup" />
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>

        <br />

        <%-- NOVO GRIDVIEW DE LIVROS (Baseado na estrutura da imagem) --%>
        <dx:ASPxGridView ID="gvGerenciamentoLivros" runat="server" ClientInstanceName="gvGerenciamentoLivros"
            KeyFieldName="LIV_ID_LIVRO" Width="100%" Theme="Office365"
            OnRowUpdating="gvGerenciamentoLivro_RowUpdating" 
            OnRowDeleting="gvGerenciamentoLivro_RowDeleting"
            OnCustomButtonCallback="gvGerenciamentoLivro_CustomButtonCallback">
            
            <SettingsEditing Mode="Batch" />
            <Settings ShowStatusBar="Hidden" />
            
            <ClientSideEvents EndCallback="OnEndCallback" />

            <Columns>
                <%-- ID Oculto --%>
                <dx:GridViewDataTextColumn FieldName="LIV_ID_LIVRO" Caption="Id" Visible="false" ReadOnly="True" />

                <dx:GridViewDataTextColumn FieldName="LIV_NM_TITULO" Caption="Título" Width="30%">
                    <PropertiesTextEdit MaxLength="100">
                        <ValidationSettings RequiredField-IsRequired="true" />
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="LIV_VL_PRECO" Caption="Preço" Width="15%">
                    <PropertiesTextEdit DisplayFormatString="N2" />
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="LIV_ID_EDICAO" Caption="Edição" Width="10%">
                    <PropertiesTextEdit MaxLength="5" />
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="LIV_PC_ROYALTY" Caption="Royalty (%)" Width="15%">
                    <PropertiesTextEdit MaxLength="5" />
                </dx:GridViewDataTextColumn>

                <%-- Coluna de Comandos --%>
                <dx:GridViewCommandColumn ShowEditButton="True" ShowDeleteButton="True" VisibleIndex="10" Caption="Ações" Width="30%">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Vendas" />
                        <dx:GridViewCommandColumnCustomButton ID="btnAutorInfo" Text="Informação" />
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
        </dx:ASPxGridView>

    </div>
</asp:Content>