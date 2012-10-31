<%@ Page Title="HairSlayer.com - My Profile" Language="C#" MasterPageFile="~/Header.Master"
    AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="HairSlayer.Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="NavBar" Src="~/UserControls/NavBar.ascx" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta name="keywords" content="Barber, Barbers, Salon, Salons, Hairstylist, Hairstyle" />


    <script language="javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="https://api.filepicker.io/v1/filepicker.js"></script>
    <script type="text/javascript">
        filepicker.setKey('AwKjJvjd1SyOaN3dRcwCkz');
        function myFunction() {
            filepicker.pick({ 'mimetype': "image/*" }, function (fpfile) {
               
                filepicker.convert(fpfile,
                { width: 180, height: 180, fit: 'crop', align: 'faces' },
                function (bigfp) {
                    $("#imgprofilepic").attr("src", bigfp.url);
                    $("#conversion1").attr("src", bigfp.url);

                    document.getElementById("<%= hdnpic1.ClientID %>").value=bigfp.url ;
               
                  
                       
                                });
            });
        }
    </script>
    <script language="JavaScript">
        //!--
        //        function autoResize(id) {
        //            var newheight;
        //            var newwidth;

        //            if (document.getElementById) {
        //                newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
        //                newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
        //            }

        //            document.getElementById(id).height = (newheight + 40) + "px";
        //            document.getElementById(id).width = (newwidth) + "px";
        //        }
        ////-->

        jQuery(document).ready(function () {
            jQuery("#MainBody_hashvalue").val(window.location.hash);
        });

        var SelectFBPhoto = function (ctrl) {
            $('#MainBody_photoid').val($(ctrl).attr("alt"));
            $('#select-fb-photo').hide();
            $('#MainBody_btnDoUpload').click();
        }

        var SelectFBPhoto2 = function (ctrl) {
            $('#MainBody_photoid').val($(ctrl).attr("alt"));
            $('#select-fb-photo').hide();
            $('#MainBody_btnUpload').click();
        }
    </script>
    <script type="text/javascript" language="javascript" src="js/fb.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="server">
    <div id="select-fb-photo"></div>
    <div id="social_cover"></div>
    <div id="content_wrap">
        <div id="bread_crumb">
            ov<a href="index.aspx">Home</a> > <a href="#">Sign Up</a> > <a href="#">Providers</a>
        </div>
        <div id="info_navigation">
            <asp:Button ID="About" runat="server" OnClick="About_Click" CssClass="info_active"
                Text="About" />
            <asp:Button ID="Styles" Visible="false" CssClass="menu-tab" runat="server" Text="My Styles" />
            <asp:Button ID="Service" runat="server" OnClick="Service_Click" CssClass="menu-tab" Text="My Services" />
            <asp:Button ID="Upload" runat="server" OnClick="Upload_Click" CssClass="menu-tab" Text="Upload My Do" />
            <asp:Button ID="Schedule" runat="server" CssClass="menu-tab" Text="My Schedule" OnClick="Schedule_Click" />
            <asp:Button ID="Clients" runat="server" Visible="false" CssClass="menu-tab" Text="My Clients" />
            <asp:Button ID="ViewProfile" runat="server" CssClass="menu-tab" Text="View Profile"
                OnClick="ViewProfile_Click" />
        </div>
        <asp:Panel ID="pnlAbout" runat="server">
            <asp:Panel ID="pnlUploadPic" runat="server">
                <div id="wrap_photo_and_button">
                    <div id="photo_area">
                        <%--    <asp:Image ID="imgProfilePic" ImageUrl="" Width="320px" Height="200px"
                         AlternateText="Profile Pic" runat="server" />--%>
                       
                        <img alt="choose pic" id="imgprofilepic" />
                        <br />
                          <input id="conversion" onclick="myFunction()" name=""></input>
                       
                    </div>
                   <%-- <div id="upload_photo_wrap">
                       
                      
                      
                    </div>--%>
                    &nbsp;&nbsp;&nbsp;
                    <div style="clear: both;"></div>
                </div>
            </asp:Panel>
            <asp:Panel ID="UploadProfile" Visible="false" Style="background-color: #FFFFFF; width: 715px; padding: 15px 15px 15px 15px; border: 1px solid #000000;"
                runat="server">
                <div class="cleared"></div>
                <asp:Label ID="Error" ForeColor="Red" Visible="False" runat="server"></asp:Label><br />
                <asp:Panel ID="pnlUpload" runat="server">
                    <%-- <input id="conversion1" onclick="myFunction()" name="" />
               
                   <asp:Button ID="uploadimage" runat="server" CssClass="red-site-button" Text="Upload" OnClick="uploadimage_Click" />--%>
                    <%--    <a href="#" onclick="fb_login2();"><img src="images/fb_photo.png" border="0" alt="" /></a>
                    <div id="fb-root"></div>
                    <br /><br />
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <br /><br />
                    <asp:Button ID="btnUpload" runat="server" CssClass="red-site-button" OnClick="btnUpload_Click"
                        Text="Upload" />--%>
                </asp:Panel>
                <%--<asp:Panel ID="pnlCrop" Visible="false" runat="server">
                    <asp:Image ID="Image1" ImageUrl="images/spacer.gif" runat="server" />
                    <asp:Image ID="Image2" ImageUrl="images/spacer.gif" runat="server" />
                    <cs:CropImage ID="wci1" runat="server" Image="Image1" Ratio="35/22" X="0" Y="0" X2="105"
                        Y2="66" CropButtonID="btnCrop" IsInUpdatePanel="true" />
                    <asp:HiddenField ID="tmp_fle" runat="server" />
                    <asp:HiddenField ID="fle_id" runat="server" />
                    <asp:Button ID="btnCrop" runat="server" OnClick="btnCrop_Click" CssClass="site-button"
                        Text="Crop Image" />
                    <asp:Button ID="btnGoBack" runat="server" Visible="False" CssClass="site-button"
                        Style="margin-right: 40px;" Text="< Go Back" OnClick="btnGoBack_Click" />
                    <asp:Button ID="btnSave2" runat="server" Visible="False" CssClass="site-button" Text="Save Image"
                        OnClick="btnSave2_Click" />
                </asp:Panel>--%>
                <%--       <asp:Button ID="btnDone" runat="server" Visible="false" CssClass="site-button" Text="Done" />
                <div style="text-align: right; margin-right: 5px;">
                    <asp:LinkButton ID="btnExit" Font-Bold="true" Font-Underline="false" Style="color: #FFFFFF;
                        background-color: #FF0000; border: 1px solid #FFFFFF; padding: 2px 5px 2px 5px;"
                        runat="server" OnClick="btnExit_Click">X</asp:LinkButton></div>--%>
            </asp:Panel>

            <div id="my_info">
                <asp:Label ID="Status" ForeColor="#FF0000" Visible="false" Style="margin: 10px 0px 15px 0px;"
                    runat="server" Text=""></asp:Label>
                <div class="form_column">

                    <div class="wrap_form_input">
                        <label>First Name:</label><br />
                        <asp:TextBox ID="Firstname" CssClass="stylist_name" runat="server"></asp:TextBox>
                    </div>
                    <div class="wrap_form_input">
                        <label>Last Name:</label><br />
                        <asp:TextBox ID="Lastname" CssClass="stylist_name" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form_column">
                    <div class="long_wrap">
                        <label>
                            Email:</label><br />
                        <asp:TextBox ID="Email" CssClass="long_input" runat="server"></asp:TextBox>
                    </div>
                </div>
                <asp:Panel ID="pnlShop" Visible="false" CssClass="shop-info" runat="server">
                    <h2 id="shop_details">Shop Details</h2>
                    <div class="form_column">
                        <div class="long_wrap">
                            <label>
                                Shop Type:</label><br>
                            <asp:DropDownList ID="ddlServicePreference" CssClass="long_input" runat="server">
                                <asp:ListItem Value="">-Select-</asp:ListItem>
                                <asp:ListItem Value="M">Barber</asp:ListItem>
                                <asp:ListItem Value="F">Stylist</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form_column">
                        <div class="long_wrap">
                            <label>
                                Shop Name:</label><br>
                            <asp:TextBox ID="Shopname" CssClass="long_input" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form_column">
                        <div class="long_wrap">
                            <label>
                                Website:</label><br>
                            <asp:TextBox ID="Website" CssClass="long_input" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form_column">
                        <div class="long_wrap">
                            <label>
                                Facebook URL:</label><br />
                            <asp:TextBox ID="Facebook" CssClass="long_input" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form_column">
                        <div class="long_wrap">
                            <label>
                                Twitter:</label><br />
                            <asp:TextBox ID="Twitter" CssClass="long_input" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form_column">
                        <div class="long_wrap">
                            <label>
                                Instagram Username:</label><br />
                            <asp:TextBox ID="Instagram" CssClass="long_input" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form_column">
                        <div class="long_wrap">
                            <label>
                                Phone:</label><br>
                            <asp:TextBox ID="Phone" CssClass="long_input" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form_column">
                        <div class="long_wrap">
                            <label>
                                Address:</label><br />
                            <asp:TextBox ID="Address" CssClass="long_input" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
                <div class="form_column">
                    <div class="short_wrap">
                        <label>
                            City</label><br />
                        <asp:TextBox ID="City" CssClass="short_input" runat="server"></asp:TextBox>
                    </div>
                    <div class="short_wrap" id="state_wrap">
                        <label>
                            State</label><br />
                        <asp:DropDownList ID="ddlState" runat="server">
                            <asp:ListItem Value="">-State-</asp:ListItem>
                            <asp:ListItem Value="AL">AL - Alabama</asp:ListItem>
                            <asp:ListItem Value="AK">AK - Alaska</asp:ListItem>
                            <asp:ListItem Value="AZ">AZ - Arizona</asp:ListItem>
                            <asp:ListItem Value="AR">AR - Arkansas</asp:ListItem>
                            <asp:ListItem Value="CA">CA - California</asp:ListItem>
                            <asp:ListItem Value="CO">CO - Colorado</asp:ListItem>
                            <asp:ListItem Value="CT">CT - Connecticut</asp:ListItem>
                            <asp:ListItem Value="DE">DE - Delaware</asp:ListItem>
                            <asp:ListItem Value="DC">DC - District of Columbia</asp:ListItem>
                            <asp:ListItem Value="FL">FL - Florida</asp:ListItem>
                            <asp:ListItem Value="GA">GA - Georgia</asp:ListItem>
                            <asp:ListItem Value="HI">HI - Hawaii</asp:ListItem>
                            <asp:ListItem Value="ID">ID - Idaho</asp:ListItem>
                            <asp:ListItem Value="IL">IL - Illinois</asp:ListItem>
                            <asp:ListItem Value="IN">IN - Indiana</asp:ListItem>
                            <asp:ListItem Value="IA">IA - Iowa</asp:ListItem>
                            <asp:ListItem Value="KS">KS - Kansas</asp:ListItem>
                            <asp:ListItem Value="KY">KY - Kentucky</asp:ListItem>
                            <asp:ListItem Value="LA">LA - Louisiana</asp:ListItem>
                            <asp:ListItem Value="ME">ME - Maine</asp:ListItem>
                            <asp:ListItem Value="MD">MD - Maryland</asp:ListItem>
                            <asp:ListItem Value="MA">MA - Massachusetts</asp:ListItem>
                            <asp:ListItem Value="MI">MI - Michigan</asp:ListItem>
                            <asp:ListItem Value="MN">MN - Minnesota</asp:ListItem>
                            <asp:ListItem Value="MS">MS - Mississippi</asp:ListItem>
                            <asp:ListItem Value="MO">MO - Missouri</asp:ListItem>
                            <asp:ListItem Value="MT">MT - Montana</asp:ListItem>
                            <asp:ListItem Value="NE">NE - Nebraska</asp:ListItem>
                            <asp:ListItem Value="NV">NV - Nevada</asp:ListItem>
                            <asp:ListItem Value="NH">NH - New Hampshire</asp:ListItem>
                            <asp:ListItem Value="NJ">NJ - New Jersey</asp:ListItem>
                            <asp:ListItem Value="NM">NM - New Mexico</asp:ListItem>
                            <asp:ListItem Value="NY">NY - New York</asp:ListItem>
                            <asp:ListItem Value="NC">NC - North Carolina</asp:ListItem>
                            <asp:ListItem Value="ND">ND - North Dakota</asp:ListItem>
                            <asp:ListItem Value="OH">OH - Ohio</asp:ListItem>
                            <asp:ListItem Value="OK">OK - Oklahoma</asp:ListItem>
                            <asp:ListItem Value="OR">OR - Oregon</asp:ListItem>
                            <asp:ListItem Value="PA">PA - Pennsylvania</asp:ListItem>
                            <asp:ListItem Value="PR">PR - Puerto Rico</asp:ListItem>
                            <asp:ListItem Value="RI">RI - Rhode Island</asp:ListItem>
                            <asp:ListItem Value="SC">SC - South Carolina</asp:ListItem>
                            <asp:ListItem Value="SD">SD - South Dakota</asp:ListItem>
                            <asp:ListItem Value="TN">TN - Tennessee</asp:ListItem>
                            <asp:ListItem Value="TX">TX - Texas</asp:ListItem>
                            <asp:ListItem Value="UT">UT - Utah</asp:ListItem>
                            <asp:ListItem Value="VT">VT - Vermont</asp:ListItem>
                            <asp:ListItem Value="VA">VA - Virginia</asp:ListItem>
                            <asp:ListItem Value="WA">WA - Washington</asp:ListItem>
                            <asp:ListItem Value="WV">WV - West Virginia</asp:ListItem>
                            <asp:ListItem Value="WI">WI - Wisconsin</asp:ListItem>
                            <asp:ListItem Value="WY">WY - Wyoming</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="short_wrap">
                        <label>
                            Zip</label><br />
                        <asp:TextBox ID="Zip" CssClass="short_input" MaxLength="5" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form_column_bio">
                    <label>
                        <asp:Label ID="Bio_Label" runat="server" Text="Bio:"></asp:Label></label><br>
                    <asp:TextBox ID="Bio" TextMode="MultiLine" Width="610px" Rows="6" CssClass="long_input"
                        runat="server" MaxLength="5000"></asp:TextBox>
                </div>
            </div>

            <br />
            <asp:Panel ID="pnlServicePreference" Visible="false" runat="server">
                <!-- Service Preference: -->

            </asp:Panel>
            <br />
            <asp:Button ID="btnSave" runat="server" CssClass="gen-button" OnClick="btnSave_Click"
                Text="Save Changes" />
            &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnViewProfile" Visible="false" runat="server" CssClass="find-button"
            Text="View My Profile" OnClick="btnViewProfile_Click" />
            <asp:Button ID="btnFindStyles" Visible="false" runat="server" CssClass="find-button"
                Text="Find Styles" OnClick="btnFindStyles_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlStyles" Visible="false" runat="server">
        </asp:Panel>

        <asp:Panel ID="pnlServices" Visible="false" runat="server">
            <div id="my_styles">
                <div id="input_wrap_styles">
                    <asp:Label ID="ServiceStatus" ForeColor="#FF0000" Visible="false" runat="server"
                        Text="This service has already been added.<br />"></asp:Label>
                    <label>Service Name:</label><br />
                    <asp:TextBox ID="Style" runat="server"></asp:TextBox><br />
                    <label>Price</label><br />
                    <asp:TextBox ID="StylePrice" runat="server"></asp:TextBox><br />
                    <label>Style Time (in minutes)</label><br />
                    <asp:TextBox ID="ServiceLength" runat="server"></asp:TextBox><br />
                    <asp:Button ID="btnAddService" OnClick="btnAddService_Click" runat="server" Text="Add Service" />
                </div>

                <div id="my_styles_list">
                    <asp:Panel ID="pnlYourServices" CssClass="services" runat="server">
                        <asp:PlaceHolder ID="ServicesHolder" runat="server"></asp:PlaceHolder>
                    </asp:Panel>
                </div>
                <div style="clear: both;"></div>
            </div>
        </asp:Panel>


        <asp:Panel ID="pnlUploadDo" Visible="false" runat="server">
            <p>
                <h2>
                    <asp:Label ID="doInfo" runat="server" Text="Before you upload your pic, please tell us a little more about your do."></asp:Label></h2>
                <p>
                </p>
                <asp:Label ID="DoError" runat="server" ForeColor="#FF0000" Text=""
                    Visible="false"></asp:Label>
                <br />
                <asp:Panel ID="NoUploads" runat="server" Visible="false">
                    <center>
                        <h2 id="nouploads-h2">Only 3 styles can be uploaded with a free account.
                        </h2>
                        <h2 id="nouploads-h2">To enjoy unlimited styles please upgrade your plan.</h2>
                        <br />
                        <a class="feature-link-button" href="Feature.aspx" target="_parent">Upgrade Now</a></center>
                </asp:Panel>
                <asp:Panel ID="pnlDoUpload" runat="server">
                    <table cellspacing="4" width="700">
                        <tr>
                            <td colspan="2">What is the name of your do?<br />
                                <asp:TextBox ID="DoName" runat="server" CssClass="text-field"></asp:TextBox>

                            </td>
                        </tr>

                           <tr>
                                                    <td colspan="2">Enter Client&#39;s Name<br />
                       
                          <asp:TextBox ID="txtname" runat="server" CssClass="text-field"></asp:TextBox>
                                                       
                                                    </td>
                            
                       </tr>
                        <tr>
                                                    <td colspan="2">Enter Client&#39;s Email<br />
                       
                          <asp:TextBox ID="txtemail" runat="server" CssClass="text-field"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    </td>
                            
                       </tr>
                          <tr>
                                                    <td colspan="2">Enter PhoneNO<br />
                       
                          <asp:TextBox ID="txtphone" runat="server" CssClass="text-field"></asp:TextBox>

                                                   
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtphone" ErrorMessage="Invalid Phone((xxx)-xxx-xxxx)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>

                                                   
                                                    </td>
                            
                       </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblDoServices" runat="server" Text="Description: "></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="rbtServices" runat="server" RepeatColumns="2">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblServices" runat="server" Text="Please select the service associated with this style."></asp:Label><br />
                                <asp:DropDownList ID="Service_Style" runat="server" CssClass="text-field">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />

                    <%--   <a href="#" onclick="fb_login();"><img src="images/fb_photo.png" border="0" alt=""></a>--%>
                    <div id="fb-root"></div>
                    <br />

                       <img alt="choose pic" id="conversion1" />
                    <input id="6h" onclick="myFunction()" />
                
                    <asp:HiddenField ID="hdnpic1" runat="server" />

                 
                    <asp:Button ID="savepic" runat="server" OnClick="savepic_Click" Text="Upload Do" ValidationGroup="a"/>
                    <asp:Label ID="lblstatus" runat="server" Visible="false"></asp:Label>

                   
                    <%--                <asp:FileUpload ID="DoFileUpload" runat="server" />--%>

                    <br />
                    <br />
                    <%--                <asp:Button ID="btnDoUpload" runat="server" CssClass="red-site-button" OnClick="btnDoUpload_Click" Text="Upload" />--%>
                </asp:Panel>
                <%--<asp:Panel ID="pnlDoCrop" runat="server" Visible="false">
                <asp:Image ID="DoImage1" runat="server" />
                <asp:Image ID="DoImage2" runat="server" ImageUrl="images/spacer.gif" />
                <cs:CropImage ID="Dowci1" runat="server" CropButtonID="btnCrop" 
                    Image="DoImage1" Ratio="1/1" X="0" X2="100" Y="0" Y2="100" />
                <br />
                <asp:Button ID="UploadAnotherStyle" runat="server" Visible="false" CssClass="site-button" 
                    onclick="UploadAnotherStyle_Click" Text="Upload Another Style" />
                <asp:HyperLink ID="ViewDoProfile" runat="server" CssClass="site-button" 
                    Target="_parent" Visible="false">View Profile</asp:HyperLink>
                <asp:Button ID="btnDoCrop" runat="server" CssClass="site-button" 
                    OnClick="btnDoCrop_Click" Text="Crop Image" />
                <asp:Button ID="btnDoGoBack" runat="server" CssClass="site-button" 
                    onclick="btnDoGoBack_Click" style="margin-right: 40px;" Text="&lt; Go Back" 
                    Visible="false" />
                <asp:Button ID="ReUpload" runat="server" CssClass="site-button" 
                    onclick="ReUpload_Click" Text="Upload Photo Again" Visible="false" />
                <asp:Button ID="btnDoSave" runat="server" CssClass="site-button" 
                    onclick="btnDoSave_Click" Text="Save Image" Visible="false" />
            </asp:Panel>--%>
                <asp:TextBox ID="loc" runat="server" Visible="false" BorderColor="#FFFFFF" ForeColor="#FFFFFF"
                    Width="1px"></asp:TextBox>
                <asp:HiddenField ID="tmp_fle_do" runat="server" />
                <asp:HiddenField ID="fle_id_do" runat="server" />
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
        </asp:Panel>

        <asp:Panel ID="pnlSchedule" Visible="false" runat="server">
            <asp:Button ID="btnSchedule" runat="server" Style="margin-top: 40px;" ForeColor="White"
                Font-Size="30px" BackColor="#CCCCCC" BorderStyle="Solid" Text="Launch Schedule Manager"
                BorderColor="#999999" BorderWidth="2px" OnClick="btnSchedule_Click" />
            <br />
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlClients" Visible="false" runat="server">
            <h2>Clients</h2>
        </asp:Panel>
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:Button ID="btnBack" Visible="false" runat="server" Text="< Back" CssClass="back-button"
                        OnClick="btnBack_Click" />
                </td>
                <td align="right">
                    <asp:Button ID="btnNext" runat="server" Text="Next >" CssClass="next-button" OnClick="btnNext_Click"
                        Visible="False" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="ShopID" Value="0" runat="server" />
        <asp:HiddenField ID="ShopGeoLocation" runat="server" />
        <asp:HiddenField ID="Locale" Value="1" runat="server" />
        <asp:HiddenField ID="DeleteShopID" runat="server" />
    </div>
    <asp:HiddenField ID="hashvalue" Value="" runat="server" />
    <asp:HiddenField ID="photoid" runat="server" />
    <div id="sidebar">
        <uc1:NavBar ID="NavigationMenu" runat="server"></uc1:NavBar>
    </div>
    <div class="cleared"></div>

</asp:Content>
