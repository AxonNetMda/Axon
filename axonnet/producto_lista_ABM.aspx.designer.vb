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


Partial Public Class producto_lista_ABM

    '''<summary>
    '''Control lblTitulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTitulo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control PanelDatos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelDatos As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtidProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtidProducto As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control txtIdProd.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtIdProd As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCodigoBarras.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCodigoBarras As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtNombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNombre As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control cboCategoria.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cboCategoria As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cboMarcas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cboMarcas As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cboProveedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cboProveedor As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cboCatalogo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cboCatalogo As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cboOferta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cboOferta As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cboDestacado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cboDestacado As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtUltimaCompra.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtUltimaCompra As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control HiddenField4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HiddenField4 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control txtNotas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNotas As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtStockCritico.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtStockCritico As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control cboEstado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cboEstado As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtIdEstado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtIdEstado As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control txtPrecioCosto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrecioCosto As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtAlicuotaIVA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtAlicuotaIVA As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblImporteIVA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblImporteIVA As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblTotalCosto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTotalCosto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtGanancia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtGanancia As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblImporteGanancia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblImporteGanancia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblTotalPrecioVenta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTotalPrecioVenta As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Label1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label1 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen1 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload1 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg1 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen1 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen1 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen1 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen2 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload2 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen2 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen2 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen2 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen3 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload3 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg3 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen3 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen3 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen3 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen4 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload4 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg4 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen4 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen4 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen4 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen5 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload5 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg5 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen5 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen5 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen5 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen6 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload6 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg6 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen6 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen6 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen6 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen7.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen7 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload7.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload7 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg7.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg7 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen7.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen7 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen7.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen7 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen7.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen7 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen8.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen8 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload8.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload8 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg8.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg8 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen8.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen8 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen8.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen8 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen8.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen8 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen9.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen9 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload9.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload9 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg9.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg9 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen9.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen9 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen9.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen9 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen9.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen9 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen10.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen10 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload10.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload10 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg10.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg10 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen10.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen10 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen10.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen10 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen10.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen10 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen11.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen11 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload11.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload11 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg11.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg11 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen11.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen11 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen11.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen11 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen11.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen11 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control imagen12.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imagen12 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control fileUpload12.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fileUpload12 As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control btnDelImg12.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnDelImg12 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hNombreImagen12.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hNombreImagen12 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hRutaimagen12.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hRutaimagen12 As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblimagen12.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblimagen12 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Button1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Button1 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Button2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Button2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control lblMensajeAtencion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMensajeAtencion As Global.System.Web.UI.WebControls.Label
End Class
