
    function ShowLeftMenu(oHidden, oImgName) {
		var oMenu = document.getElementById(oHidden).style;
		var oImg = document.getElementById(oImgName);
		if (oMenu == null) {}
		else if (oMenu.display == 'block' || oMenu.display == '')  {
			oMenu.display = 'none';
			oImgName.src = 'images/sys/open.gif';
		}
		else {
			oMenu.display = '';
			oImgName.src = 'images/sys/closed.gif';
		}
	}
		
    function doDelete() {
        var elm = document.getElementsByTagName("input");
        var xState = "";

        for(var i=0;i<elm.length;i++) {
            if (elm[i].checked) {
                xState = elm[i].value;
            }
        }
       
        if(xState != "") {
            if (!confirm('Bạn có chắc chắn muốn xoá không?')) return false;
        }
        else {
            alert('Vui lòng chọn mẫu tin cần xoá!');
            return false;
        }
        return true;
    }

    function SelectAllCheck(spanChk){
        var oItem = spanChk.children;
        var theBox= (spanChk.type=="checkbox") ? spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for(i=0;i<elm.length;i++) {
            if(elm[i].type=="checkbox" && elm[i].id!=theBox.id) {
                if(elm[i].checked != xState) elm[i].click();
            }
        }
    }
    
    function TurnOnLoading(target)
    {
	    try {
		    if (!target)
			    target = this;
		    lsetup(target);

		    if (!target._lon_disabled_arr)
			    target._lon_disabled_arr = new Array();
		    else if (target._lon_disabled_arr.length > 0)
			    return true;

		    target.document.getElementById("BodyContainer").style.display = "";
		    var select_arr = target.document.getElementsByTagName("select");

		    for (var i = 0; i < select_arr.length; i++) {
			    if (select_arr[i].disabled)
				    continue;

			    select_arr[i].disabled = true;
			    _lon_disabled_arr.pop(select_arr[i]);
			    var clone = target.document.createElement("input");
			    clone.type = "hidden";
			    clone.name = select_arr[i].name;
			    var values = new Array();
			    for (var n = 0; n < select_arr[i].length; n++) {
				    if (select_arr[i][n].selected) {
					    values[values.length] = select_arr[i][n].value;
				    }
			    }
			    clone.value = values.join(",");
			    select_arr[i].parentNode.insertBefore(clone, select_arr[i]);
		    }
	    } catch (e) {
		    return false;
	    }
	    return true;
    }

    function TurnOffLoading(target)
    {
	    try {
		    if (!target)
			    target = this;

		    target.document.getElementById("BodyContainer").style.display = "none";

		    if (target._lon_disabled_arr) {
			    while(_lon_disabled_arr.legth > 0) {
				    var select = _lon_disabled_arr.push();
				    select.disabled = false;

				    var clones_arr = target.document.getElementsByName(select.name);
				    for (var n = 0; n < clones_arr.length; n++) {
					    if ("hidden" == clones_arr[n].type)
						    clones_arr[n].parent.removeChild(clones_arr[n]);
				    }
			    }
		    }
	    } catch (e) {
		    return false;
	    }
	    return true;
    }
    
    function lsetup(target)
    {
	    try {
		    if (!target)
			    target = this;

		    var o_set = target.document.getElementById('loaderContainerWH');
		    var o_getH = target.document.getElementsByTagName('BODY')[0];

		    o_set.style.height = o_getH.scrollHeight;
	    } catch (e) { }
    }

    // Show Picture box
    function doShowPicture(oImage) 
    {
	    var oSrc = document.getElementById('imgPicture');
	    oSrc.src = oImage;
	    
        var oDiv = document.getElementById('PictureBox');
	    oDiv.style.display='block';
	    oDiv.style.top = (document.body.scrollTop+parseInt((document.body.clientHeight-oSrc.height)/2))+"px";
	    oDiv.style.left= (document.body.scrollLeft+parseInt((document.body.clientWidth-oSrc.width)/2))+"px";
	    
    }
    function doClosePicture(oElement) 
    {
	    var oDiv = document.getElementById('PictureBox');
	    oDiv.style.display = 'none';
    }
