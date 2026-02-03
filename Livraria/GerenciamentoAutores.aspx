 <%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GerenciamentoAutores.aspx.cs" Inherits="ProjetoLivrariaTrends.Livraria.GerenciamentoAutores" %>

<%-- 
    1. ÁREA DO CABEÇALHO (Scripts e CSS)
    .
--%>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnEndCallback(s, e) {
            if (s.cpRedirectToLivros) {
                window.location.href = '/Livraria/GerenciamentoAutores.aspx';
            }
        }
    </script>
</asp:Content>

<%--    Aqui fica apenas o que o usuário VÊ.
--%>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="Cadastro de Autores" ColCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    
                    <%-- Campo Nome --%>
                    <dx:LayoutItem Caption="Nome">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroNomeAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Nome obrigatório!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- Campo Sobrenome --%>
                    <dx:LayoutItem Caption="Sobrenome">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroSobrenomeAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Sobrenome obrigatório!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- Campo E-Mail --%>
                    <dx:LayoutItem Caption="E-Mail" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroEmailAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Email obrigatório!" />
                                        <RegularExpression ErrorText="Email inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- Botão Salvar --%>
                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnSalvar" Text="Salvar" AutoPostBack="True" Width="100%" OnClick="BtnNovoAutor_Click" ValidationGroup="MyGroup" />
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    
                    <%-- GridView --%>
                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView ID="gvGerenciamentoAutores" runat="server" 
                                    Width="100%" KeyFieldName="AUT_ID_AUTOR" Theme="Office365"
                                    OnRowUpdating="gvGerenciamentoAutores_RowUpdating"
                                    OnRowDeleting="gvGerenciamentoAutores_RowDeleting"
                                    OnCustomButtonCallback="gvGerenciamentoAutores_CustomButtonCallback">
                                    <ClientSideEvents EndCallback="OnEndCallback" />

                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="AUT_ID_AUTOR" Caption="Id" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="AUT_NM_NOME" Caption="Nome" />
                                        <dx:GridViewDataTextColumn FieldName="AUT_NM_SOBRENOME" Caption="Sobrenome" />
                                        <dx:GridViewDataTextColumn FieldName="AUT_DS_EMAIL" Caption="Email" />
                                        <dx:GridViewCommandColumn ShowEditButton="True" ShowDeleteButton="True">

                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Livros"/>
                                                <dx:GridViewCommandColumnCustomButton ID="btnAutorInfo" Text="Informação"/>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                   <Settings ShowFilterRow="False" ShowGroupPanel="True" />
                                    <SettingsEditing Mode="inline" />
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>