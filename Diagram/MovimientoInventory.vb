'------------------------------------------------------------------------------
' <auto-generated>
'     Este código se generó a partir de una plantilla.
'
'     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
'     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class MovimientoInventory
    Public Property Id As Integer
    Public Property TypeMoviment As Integer
    Public Property Quantity As Integer
    Public Property [Date] As Integer
    Public Property Description As Integer
    Public Property IdInventory As String
    Public Property UserId As Integer
    Public Property ProductId As Integer

    Public Overridable Property Inventory As Inventory
    Public Overridable Property User As User
    Public Overridable Property Product As Product

End Class
