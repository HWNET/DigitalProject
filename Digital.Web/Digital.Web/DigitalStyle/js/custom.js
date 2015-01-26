
String.prototype.gblen = function () {
    var len = 0;
    for (var i = 0; i < this.length; i++) {
        if (this.charCodeAt(i) > 127 || this.charCodeAt(i) == 94) {
            len += 2;
        } else {
            len++;
        }
    }
    return len;
}
String.prototype.gbtrim = function (len, s) {
    var str = '';
    var sp = s || '';
    var len2 = 0;
    for (var i = 0; i < this.length; i++) {
        if (this.charCodeAt(i) > 127 || this.charCodeAt(i) == 94) {
            len2 += 2;
        } else {
            len2++;
        }
    }
    if (len2 <= len) {
        return this;
    }
    len2 = 0;
    len = (len > sp.length) ? len - sp.length : len;
    for (var i = 0; i < this.length; i++) {
        if (this.charCodeAt(i) > 127 || this.charCodeAt(i) == 94) {
            len2 += 2;
        } else {
            len2++;
        }
        if (len2 > len) {
            str += sp;
            break;
        }
        str += this.charAt(i);
    }
    return str;
}


function SaveInfo(IsSuccess, msg) {
    if (IsSuccess) {
        jQuery.gritter.add({
            title: '系统提示!',
            text: msg + '保存成功.',
            sticky: false,
            time: ''
        });
    }
    else {
        jQuery.gritter.add({
            title: '系统提示!',
            text: msg + '保存失败.',
            sticky: false,
            time: ''
        });
    }

}

function StringCombine(element) {
    var stringTemp = "";
    var strIndex = 0;
    $(element).each(function () {
        if (strIndex > 0) {
            if ($(this).val() != "") {
                stringTemp += ",";
            }
        }
        stringTemp += $(this).val();
        ++strIndex;
    });
    return stringTemp;
}

function DropOptionDefault(SelectedId)
{
    SelectedId = SelectedId > 0 ? SelectedId : 1;
    return SelectedId.toString();
}
function DropOptionFolders(objson, selectobj, SelectedId)
{
    var obj = eval(objson);
    $(obj).each(function (index) {
        var val = obj[index];
        var HtmlSelect = "";
        if (typeof (val.FolderName) != "undefined") {
            if (index == SelectedId) {
                HtmlSelect = "selected";
            }

            selectobj.append("<option value='" + val.FolderNameCode + "' " + HtmlSelect + ">" + val.FolderName + "</option>");
        }
    });
}
function DropOption(objson, selectobj, SelectedId)
{
    var obj = eval(objson);
    

    var SelectIds = SelectedId.toString().split(",");
    if (SelectIds.length == 1) {
        
        SelectIds = SelectedId > 0 ? SelectedId : 1;
    }
    $(obj).each(function (index) {
        var val = obj[index];
        if (val.Id == -1 && val.Name == "LINE") {
            selectobj.append("<optgroup label=\"----------------------\">");
        }
        else if (val.Id == -1 && val.Name == "END")
        {
            
            selectobj.append("</optgroup>");
            //</optgroup>
        }
        else
        {
           
            var HtmlSelect = "";
            if (typeof (val.Id) != "undefined") {
                if ($.inArray(val.Id.toString(), SelectIds) != -1) {
                    HtmlSelect = "selected"
                }
                else if (val.Id == SelectIds) {
                    HtmlSelect = "selected"
                }
                //if (SelectIds == val.Id)
                //{
                //    HtmlSelect = "selected"
                //}
                selectobj.append("<option value='" + val.Id + "' " + HtmlSelect + ">" + val.Name + "</option>");
            }
    }
       
    });
}

function RadioHtml(objson, selectobj, SelectedId,className,RadioName)
{
    var obj = eval(objson);
    SelectedId = SelectedId > 0 ? SelectedId : 1;
    $(obj).each(function (index) {
        var val = obj[index];
        
            var HtmlSelect = "";
            if (SelectedId == val.Id) {
                HtmlSelect = "checked = 'checked'"
            }
            var RadioId=RadioName+index;
        selectobj.append("<div class=\""+className+"\"><input type=\"radio\" name=\""+RadioName+"\" id=\""+RadioId+"\" value="+val.Id+" "+HtmlSelect+"/><label for=\""+RadioId+"\">"+val.Name+"</label></div>");
        

    });
}

function UILIHtml(objson, selectobj)
{
    var obj = eval(objson);
    //<li><a href=""><i class="fa fa-folder-o"></i> 产品图片</a></li>
    $(obj).each(function (index) {
        var val = obj[index];

        selectobj.append("<li><a href=\"#\" onclick=\"javascript:FileListByFolder('" + val.FolderNameCode + "');\"><i class=\"fa fa-folder-o\"></i> " + val.FolderName + "</a></li>");
    });
}

function ComAjaxGet(Url, XmlNodeParent,XmlNodeChild,SelectObjId, SelectedObjOption, SelectObjChildId, SelectObjChildOption,IsCity)
{
    var AttributeID = IsCity == 1 ? "ID" : "Id";
    var AttributeName = "Name";

    jQuery.ajax({
        url: Url, 
        type: "get",
        datatype: "xml",
        success: function (xmldoc) {
            jQuery(xmldoc).find(XmlNodeParent).each(function () {
                if (SelectedObjOption == jQuery(this).attr(AttributeID)) {
                    jQuery("#" + SelectObjId).append("<option selected value='" + jQuery(this).attr(AttributeID) + "'>" + jQuery(this).attr(AttributeName) + "</option>");
                    ComAjaxGetForParent(jQuery(this).attr(AttributeID), Url, XmlNodeParent, XmlNodeChild, SelectObjChildId, SelectObjChildOption, IsCity);
                }
                else {
                    jQuery("#" + SelectObjId).append("<option value='" + jQuery(this).attr(AttributeID) + "'>" + jQuery(this).attr(AttributeName) + "</option>");
                }
            })

            jQuery("#" + SelectObjId).chosen({ 'width': '100%', 'white-space': 'nowrap' });
            jQuery("#" + SelectObjId).trigger("chosen:updated");
        }
    });
}

function ComAjaxGetForParent(SelectCategory, Url, XmlNodeParent, XmlNodeChild, SelectObjChildId, SelectObjChildOption, IsCity)
{
    var AttributeID = IsCity == 1 ? "ID" : "Id";
    var AttributeName = "Name";

    jQuery.ajax({
        url: Url, 
        type: "get",
        datatype: "xml",
        success: function (xmldoc) {
            jQuery("#" + SelectObjChildId).find("option").remove();
            jQuery("#" + SelectObjChildId).append("<option  value='0'> --请选择-- </option>");
            jQuery(xmldoc).find("Root>" + XmlNodeParent + "[" + AttributeID + "=" + SelectCategory + "]>" + XmlNodeChild + "").each(function () {
                if (SelectObjChildOption == jQuery(this).attr(AttributeID)) {
                    if (IsCity==1) {
                        jQuery("#" + SelectObjChildId).append("<option selected value='" + jQuery(this).attr(AttributeID) + "'>" +jQuery(this).text() + "</option>");
                    }
                    else {
                        jQuery("#" + SelectObjChildId).append("<option selected value='" + jQuery(this).attr(AttributeID) + "'>" + jQuery(this).attr(AttributeName) + "</option>");
                    }
                    
                }
                else {
                    if (IsCity == 1) {
                        jQuery("#" + SelectObjChildId).append("<option value='" + jQuery(this).attr(AttributeID) + "'>" + jQuery(this).text()  + "</option>");
                    }
                    else {
                        jQuery("#" + SelectObjChildId).append("<option value='" + jQuery(this).attr(AttributeID) + "'>" + jQuery(this).attr(AttributeName) + "</option>");
                    }
                    
                }
            });

            jQuery("#" + SelectObjChildId).chosen({ 'width': '100%', 'white-space': 'nowrap' });
            jQuery("#" + SelectObjChildId).trigger("chosen:updated");
        }
    });
}

function ComAjax(Url,datas,SaveMsg,callback)
{
    $.ajaxAntiForgery({
        type: "post",
        data: datas,
        url: Url,
        success: function (data) {
            if (data == "OK") {
                if (callback != null && typeof (callback) != "undefined") {
                    eval(callback+"()");
                }
                SaveInfo(true, SaveMsg);
            }
            else {
                SaveInfo(false, SaveMsg)
            }
        }
    })
}

function ComAjaxJson(Url,Datas,Callback)
{
    $.ajaxAntiForgery({
        type: "post",
        data: Datas,
        url: Url,
        dataType: "json",
        success: function (data) {
            if (Callback != null && typeof (Callback) != "undefined") {
                Callback(data);
            }
        }
    });
}

jQuery.jsparams = {
    input: function(Id){
        return $("#" + Id).val();
    },
    input_multiple: function (Id) {
        var inputList = $('input[id*=' + Id + ']');
        return StringCombine(inputList);
    },
    textarea: function (Id) {
        return $("#" + Id).val();
    },
    select: function(Id){
        return $("#" + Id + "  option:selected").val();
    },
    select_multiple:function(Id){
        var optionList=$("#" + Id + "  option:selected");
        return StringCombine(optionList);
    },
    check: function (Id) {
        return $("#" + Id).attr("checked") == "checked" ? true : false;
    },
    checkByName: function(Name)
    {
        var Ids = "";
        $("input[name='" + Name + "']").each(function (index, domEle) {
            if ($(this).attr("checked") == "checked") {
                Ids += $(domEle).val() + ",";
            }
        });
        return Ids;
    },
    checkAll:function(Name)
    {

        $("input[name='" + Name + "']").each(function (index, domEle) {
          
            $(domEle).attr('checked', true);

        });
    },
    radio: function (name) {
        return $('input:radio[name=' + name + ']:checked').val();
    }
};

jQuery(window).load(function () {
   
   // Page Preloader
   jQuery('#status').fadeOut();
   jQuery('#preloader').delay(350).fadeOut(function(){
      jQuery('body').delay(350).css({'overflow':'visible'});
   });
});

jQuery(document).ready(function() {
   
   // Toggle Left Menu
   jQuery('.leftpanel .nav-parent > a').live('click', function() {
      
      var parent = jQuery(this).parent();
      var sub = parent.find('> ul');
      
      // Dropdown works only when leftpanel is not collapsed
      if(!jQuery('body').hasClass('leftpanel-collapsed')) {
         if(sub.is(':visible')) {
            sub.slideUp(200, function(){
               parent.removeClass('nav-active');
               jQuery('.mainpanel').css({height: ''});
               adjustmainpanelheight();
            });
         } else {
            closeVisibleSubMenu();
            parent.addClass('nav-active');
            sub.slideDown(200, function(){
               adjustmainpanelheight();
            });
         }
      }
      return false;
   });
   
   function closeVisibleSubMenu() {
      jQuery('.leftpanel .nav-parent').each(function() {
         var t = jQuery(this);
         if(t.hasClass('nav-active')) {
            t.find('> ul').slideUp(200, function(){
               t.removeClass('nav-active');
            });
         }
      });
   }
   
   function adjustmainpanelheight() {
      // Adjust mainpanel height
      //var docHeight = jQuery(document).height();
      //if(docHeight > jQuery('.mainpanel').height())
      //   jQuery('.mainpanel').height(docHeight);
   }
   adjustmainpanelheight();
   
   
   // Tooltip
   jQuery('.tooltips').tooltip({ container: 'body'});
   
   // Popover
   jQuery('.popovers').popover();
   
   // Close Button in Panels
   jQuery('.panel .panel-close').click(function(){
      jQuery(this).closest('.panel').fadeOut(200);
      return false;
   });
   
   // Form Toggles
   jQuery('.toggle').toggles({on: true});
   
   jQuery('.toggle-chat1').toggles({on: false});
   
   // Sparkline
   jQuery('#sidebar-chart').sparkline([4,3,3,1,4,3,2,2,3,10,9,6], {
	  type: 'bar', 
	  height:'30px',
      barColor: '#428BCA'
   });
   
   jQuery('#sidebar-chart2').sparkline([1,3,4,5,4,10,8,5,7,6,9,3], {
	  type: 'bar', 
	  height:'30px',
      barColor: '#D9534F'
   });
   
   jQuery('#sidebar-chart3').sparkline([5,9,3,8,4,10,8,5,7,6,9,3], {
	  type: 'bar', 
	  height:'30px',
      barColor: '#1CAF9A'
   });
   
   jQuery('#sidebar-chart4').sparkline([4,3,3,1,4,3,2,2,3,10,9,6], {
	  type: 'bar', 
	  height:'30px',
      barColor: '#428BCA'
   });
   
   jQuery('#sidebar-chart5').sparkline([1,3,4,5,4,10,8,5,7,6,9,3], {
	  type: 'bar', 
	  height:'30px',
      barColor: '#F0AD4E'
   });
   
   
   // Minimize Button in Panels
   jQuery('.minimize').click(function(){
      var t = jQuery(this);
      var p = t.closest('.panel');
      if(!jQuery(this).hasClass('maximize')) {
         p.find('.panel-body, .panel-footer').slideUp(200);
         t.addClass('maximize');
         t.html('&plus;');
      } else {
         p.find('.panel-body, .panel-footer').slideDown(200);
         t.removeClass('maximize');
         t.html('&minus;');
      }
      return false;
   });
   
   
   // Add class everytime a mouse pointer hover over it
   jQuery('.nav-bracket > li').hover(function(){
      jQuery(this).addClass('nav-hover');
   }, function(){
      jQuery(this).removeClass('nav-hover');
   });
   
   
   // Menu Toggle
   jQuery('.menutoggle').click(function(){
      
      var body = jQuery('body');
      var bodypos = body.css('position');
      
      if(bodypos != 'relative') {
         
         if(!body.hasClass('leftpanel-collapsed')) {
            body.addClass('leftpanel-collapsed');
            jQuery('.nav-bracket ul').attr('style','');
            
            jQuery(this).addClass('menu-collapsed');
            
         } else {
            body.removeClass('leftpanel-collapsed chat-view');
            jQuery('.nav-bracket li.active ul').css({display: 'block'});
            
            jQuery(this).removeClass('menu-collapsed');
            
         }
      } else {
         
         if(body.hasClass('leftpanel-show'))
            body.removeClass('leftpanel-show');
         else
            body.addClass('leftpanel-show');
         
         adjustmainpanelheight();         
      }

   });
   
   // Chat View
   jQuery('#chatview').click(function(){
      
      var body = jQuery('body');
      var bodypos = body.css('position');
      
      if(bodypos != 'relative') {
         
         if(!body.hasClass('chat-view')) {
            body.addClass('leftpanel-collapsed chat-view');
            jQuery('.nav-bracket ul').attr('style','');
            
         } else {
            
            body.removeClass('chat-view');
            
            if(!jQuery('.menutoggle').hasClass('menu-collapsed')) {
               jQuery('body').removeClass('leftpanel-collapsed');
               jQuery('.nav-bracket li.active ul').css({display: 'block'});
            } else {
               
            }
         }
         
      } else {
         
         if(!body.hasClass('chat-relative-view')) {
            
            body.addClass('chat-relative-view');
            body.css({left: ''});
         
         } else {
            body.removeClass('chat-relative-view');   
         }
      }
      
   });
   
   reposition_topnav();
   reposition_searchform();
   
   jQuery(window).resize(function(){
      
      if(jQuery('body').css('position') == 'relative') {

         jQuery('body').removeClass('leftpanel-collapsed chat-view');
         
      } else {
         
         jQuery('body').removeClass('chat-relative-view');         
         jQuery('body').css({left: '', marginRight: ''});
      }
      
      if(jQuery('.leftpanel .searchform').length == 0)
         reposition_searchform();
      reposition_topnav();
      
   });
   
   
   
   /* This function will reposition search form to the left panel when viewed
    * in screens smaller than 767px and will return to top when viewed higher
    * than 767px
    */ 
   function reposition_searchform() {
      if(jQuery('.searchform').css('position') == 'relative') {
         jQuery('.searchform').insertBefore('.leftpanelinner .userlogged');
      } else {
         jQuery('.searchform').insertBefore('.header-right');
      }
   }
   
   

   /* This function allows top navigation menu to move to left navigation menu
    * when viewed in screens lower than 1024px and will move it back when viewed
    * higher than 1024px
    */
   function reposition_topnav() {
      if(jQuery('.nav-horizontal').length > 0) {
         
         // top navigation move to left nav
         // .nav-horizontal will set position to relative when viewed in screen below 1024
         if(jQuery('.nav-horizontal').css('position') == 'relative') {
                                  
            if(jQuery('.leftpanel .nav-bracket').length == 2) {
               jQuery('.nav-horizontal').insertAfter('.nav-bracket:eq(1)');
            } else {
               // only add to bottom if .nav-horizontal is not yet in the left panel
               if(jQuery('.leftpanel .nav-horizontal').length == 0)
                  jQuery('.nav-horizontal').appendTo('.leftpanelinner');
            }
            
            jQuery('.nav-horizontal').css({display: 'block'})
                                  .addClass('nav-pills nav-stacked nav-bracket');
            
            jQuery('.nav-horizontal .children').removeClass('dropdown-menu');
            jQuery('.nav-horizontal > li').each(function() { 
               
               jQuery(this).removeClass('open');
               jQuery(this).find('a').removeAttr('class');
               jQuery(this).find('a').removeAttr('data-toggle');
               
            });
            
            if(jQuery('.nav-horizontal li:last-child').has('form')) {
               jQuery('.nav-horizontal li:last-child form').addClass('searchform').appendTo('.topnav');
               jQuery('.nav-horizontal li:last-child').hide();
            }
         
         } else {
            // move nav only when .nav-horizontal is currently from leftpanel
            // that is viewed from screen size above 1024
            if(jQuery('.leftpanel .nav-horizontal').length > 0) {
               
               jQuery('.nav-horizontal').removeClass('nav-pills nav-stacked nav-bracket')
                                        .appendTo('.topnav');
               jQuery('.nav-horizontal .children').addClass('dropdown-menu').removeAttr('style');
               jQuery('.nav-horizontal li:last-child').show();
               jQuery('.searchform').removeClass('searchform').appendTo('.nav-horizontal li:last-child .dropdown-menu');
               jQuery('.nav-horizontal > li > a').each(function() {
                  
                  jQuery(this).parent().removeClass('nav-active');
                  
                  if(jQuery(this).parent().find('.dropdown-menu').length > 0) {
                     jQuery(this).attr('class','dropdown-toggle');
                     jQuery(this).attr('data-toggle','dropdown');  
                  }
                  
               });              
            }
            
         }
         
      }
   }
   
   
   // Sticky Header
   if(jQuery.cookie('sticky-header'))
      jQuery('body').addClass('stickyheader');
      
   // Sticky Left Panel
   if(jQuery.cookie('sticky-leftpanel')) {
      jQuery('body').addClass('stickyheader');
      jQuery('.leftpanel').addClass('sticky-leftpanel');
   }
   
   // Left Panel Collapsed
   if(jQuery.cookie('leftpanel-collapsed')) {
      jQuery('body').addClass('leftpanel-collapsed');
      jQuery('.menutoggle').addClass('menu-collapsed');
   }
   
   // Changing Skin
   var c = jQuery.cookie('change-skin');
   if(c) {
      jQuery('head').append('<link id="skinswitch" rel="stylesheet" href="css/style.'+c+'.css"/*tpa=http://themepixels.com/demo/webpage/bracket/js/css/style.'+c+'.css*/ />');
   }
   
   // Changing Font
   var fnt = jQuery.cookie('change-font');
   if(fnt) {
      jQuery('head').append('<link id="fontswitch" rel="stylesheet" href="css/font.'+fnt+'.css"/*tpa=http://themepixels.com/demo/webpage/bracket/js/css/font.'+fnt+'.css*/ />');
   }
   
   // Check if leftpanel is collapsed
   if(jQuery('body').hasClass('leftpanel-collapsed'))
      jQuery('.nav-bracket .children').css({display: ''});
      
     
   // Handles form inside of dropdown 
   jQuery('.dropdown-menu').find('form').click(function (e) {
      e.stopPropagation();
    });
      

});