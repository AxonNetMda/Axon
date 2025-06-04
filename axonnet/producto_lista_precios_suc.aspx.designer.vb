'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class producto_lista_precios_suc

    '''<summary>
    '''Control cboSucursal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cboSucursal As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cboMostrar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cboMostrar As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtBuscar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBuscar As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control divPrecios.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents divPrecios As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control dgvData.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dgvData As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control sqlProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents sqlProducto As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Control lblReferencia1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblReferencia1 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblReferencia2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblReferencia2 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblReferencia3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblReferencia3 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblMensajeAtencion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMensajeAtencion As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control udpAjuste.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents udpAjuste As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control txtidProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtidProducto As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblMDLIdSucursal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMDLIdSucursal As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblMDLSucursal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMDLSucursal As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblMDLIdProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMDLIdProducto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblMDLProductoNombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMDLProductoNombre As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblMDLStock.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMDLStock As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtMDLStockCritico.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMDLStockCritico As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMDLCantidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMDLCantidad As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAceptarMDLSave.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAceptarMDLSave As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnCerrarMDLEdit.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnCerrarMDLEdit As Global.System.Web.UI.WebControls.Button
End Class
