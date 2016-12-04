var winConfig = {
	view: "window",
 	width: 400,
 	height: 350,
 	id: "myWin",
	position: "center",
	modal: true,
	head:{
		view: "toolbar",
		elements:[
			{ view: "label", label: "Task form"},
	  		{ view: "button", type:"iconButton", label: "Close", icon: "times", click:function(){$$("myWin").hide()}, width:90}
		]
	},
 	body:{
  		view: "form",
  		id: "myForm",
  		elementsConfig:{
   		labelPosition: "top"
  	},
  		elements:[
			{ view: "text", label: "Title", name:"text"},
			{ view: "textarea", label: "Desription", name:"description", height: 90},
			{ view: "button",label:"Save", type: "form", inputWidth:120, align: "center",
    		click:function(){
		     	$$("myWin").hide();
		     	var values = $$("myForm").getValues();
		     	var id = $$("myBoard").getSelectedId();
		     	var item = $$("myBoard").getItem(id);
		     	item.text = values.text;
		     	item.description = values.description;
		     	$$("myBoard").refresh(id);
    		}}
  		]
 	}
};
// shows item data in a form
function showForm(item){
   if(!$$("myWin")){
      webix.ui(winConfig);
   }
   $$("myWin").show();
   $$("myForm").setValues(item);
   $$("myForm").focus();
}