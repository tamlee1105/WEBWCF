<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CloudTag.ascx.cs" Inherits="Website.UserControl.CloudTag" %>
<%@ Register Assembly="wpCumulus" Namespace="wpCumulus" TagPrefix="cc1" %>
<cc1:WPCumulus ID="WPCumulus1" DataCountField='Weight' DataTextField='Product' DataUrlField="HrefUrl"
    BackColor="#FFFFFF" HiColor="192, 192, 255" TagColor1="Red" TagColor2="Blue"
    Width="100%" Height="100%" runat="server" Distr="true" MinimumTagSize="12" />
