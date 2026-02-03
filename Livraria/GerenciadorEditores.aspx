<%@ Page Title="Gerenciamento de Editoras" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GerenciadorEditores.aspx.cs" Inherits="ProjetoLivrariaTrends.Livraria.GerenciadorEditores" %>

<%-- 
    1. ÁREA DO CABEÇALHO (Scripts e CSS)
--%>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        // Script para redirecionar caso clique no botão "Livros" da Grid
        function OnEndCallback(s, e) {
            if (s.cpRedirectToLivros) {
                window.location.href = '/Livraria/GerenciadorLivro.aspx';
            }
        }
    </script>
</asp:Content>

<%-- 
    2. CONTEÚDO PRINCIPAL
--%>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    
    <%-- Label para mensagens de Sucesso/Erro --%>
    <div style="padding-bottom: 10px;">
        <dx:ASPxLabel ID="lblMensagem" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></dx:ASPxLabel>
    </div>

    <dx:ASPxFormLayout ID="formLayoutEditores" runat="server" Width="75%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="Cadastro de Editoras" ColCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    
                    <%-- 1. CAMPO NOME DA EDITORA --%>
                    <dx:LayoutItem Caption="Nome da Editora" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroNomeEditor" runat="server" Width="80%">
                                    <ValidationSettings ValidationGroup="GroupEditor" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="O nome da editora é obrigatório!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- 2. CAMPO EMAIL --%>
                    <dx:LayoutItem Caption="E-Mail">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroEmailEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="GroupEditor" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="O e-mail é obrigatório!" />
                                        <RegularExpression ErrorText="E-mail inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- 3. CAMPO URL --%>
                    <dx:LayoutItem Caption="Site / URL">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroUrlEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="GroupEditor" Display="Dynamic">
                                        <%-- Validação de URL opcional --%>
                                        <RegularExpression ErrorText="URL inválida" ValidationExpression="^(https?://)?([\da-z.-]+)\.([a-z.]{2,6})([/\w .-]*)*/?$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- 4. BOTÃO SALVAR --%>
                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnSalvar" Text="Salvar Editora" 
                                    AutoPostBack="True" Width="40%" 
                                    OnClick="BtnSalvar_Click" 
                                    ValidationGroup="GroupEditor" Theme="Office365" />
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    
                    <%-- 5. GRID DE GERENCIAMENTO --%>
                    <dx:LayoutItem Caption="Editoras Cadastradas" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                
                                <%-- IMPORTANTE: EnableCallBacks="false" para o Label de mensagem funcionar --%>
                                <dx:ASPxGridView ID="gvGerenciamentoEditores" runat="server" 
                                    Width="100%" KeyFieldName="EDI_ID_EDITOR" Theme="Office365"
                                    EnableCallBacks="false"
                                    OnRowUpdating="gvGerenciamentoEditores_RowUpdating"
                                    OnRowDeleting="gvGerenciamentoEditores_RowDeleting"
                                    OnCustomButtonCallback="gvGerenciamentoEditores_CustomButtonCallback"
                                    OnPageIndexChanged="gvGerenciamentoEditores_PageIndexChanged">
                                    
                                    <ClientSideEvents EndCallback="OnEndCallback" />

                                    <Columns>
                                        <%-- Colunas do Banco de Dados --%>
                                        <dx:GridViewDataTextColumn FieldName="EDI_ID_EDITOR" Caption="Id" Visible="false" />
                                        
                                        <dx:GridViewDataTextColumn FieldName="EDI_NM_EDITOR" Caption="Nome da Editora">
                                            <PropertiesTextEdit>
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="true" ErrorText="Nome é obrigatório" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        
                                        <dx:GridViewDataTextColumn FieldName="EDI_DS_EMAIL" Caption="E-mail">
                                             <PropertiesTextEdit>
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="true" ErrorText="E-mail é obrigatório" />
                                                    <RegularExpression ErrorText="E-mail inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="EDI_DS_URL" Caption="Site / URL" />

                                        <%-- Botões de Ação --%>
                                        <dx:GridViewCommandColumn ShowEditButton="True" ShowDeleteButton="True" VisibleIndex="10">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Ver Livros"/>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                    
                                    <Settings ShowFilterRow="False" ShowGroupPanel="False" />
                                    <%-- OBRIGATÓRIO: Mode="Inline" para o botão Update funcionar --%>
                                    <SettingsEditing Mode="Inline" />
                                    <SettingsPager PageSize="10" />
                                </dx:ASPxGridView>

                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>