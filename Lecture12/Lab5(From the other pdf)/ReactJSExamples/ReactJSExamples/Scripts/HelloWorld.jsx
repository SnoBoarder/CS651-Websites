var App = React.createClass({
    render: function(){
        return(<h1> Hello World! </h1>);
    });
React.renderComponent(<App url="/ReactJS/Index" />, document.getElementById('content'));
