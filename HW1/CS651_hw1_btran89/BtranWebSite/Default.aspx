<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        html, body
        {
            background: #f2f2f2;
            overflow: hidden;
            margin: 0;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div>
            <asp:TextBox ID="_sentence" runat="server"></asp:TextBox>
            <asp:Button ID="_removeLetterButton" Text="Ask Backend: Remove the Sequence Letters:" OnClick="OnRemoveLetter" runat="server"/>
            <asp:TextBox ID="_letter" runat="server"></asp:TextBox>
        </div>

        ------------------------------------------------------------------------

        <div>
            <asp:Button Text="Ask Backend: Particles hello" OnClick="OnHello" runat="server" />
            <asp:Button Text="Ask Backend: Particles goodbye" OnClick="OnGoodBye" runat="server" />
            <asp:Button Text="Toggle Free Mode" OnClick="OnFreeMode" runat="server" />
        </div>
    </div>
    <!-- Particle effects from: http://soulwire.github.io/sketch.js/examples/particles.html -->
    <script src="sketch.js"></script>
    <script>
        // ----------------------------------------
        // Particle
        // ----------------------------------------

        function Particle( x, y, radius ) {
            this.init( x, y, radius );
        }

        Particle.prototype = {

            init: function( x, y, radius ) {

                this.alive = true;

                this.radius = radius || 10;
                this.wander = 0.15;
                this.theta = random( TWO_PI );
                this.drag = 0.92;
                this.color = '#fff';

                this.x = x || 0.0;
                this.y = y || 0.0;

                this.vx = 0.0;
                this.vy = 0.0;
            },

            move: function() {

                this.x += this.vx;
                this.y += this.vy;

                this.vx *= this.drag;
                this.vy *= this.drag;

                this.theta += random( -0.5, 0.5 ) * this.wander;
                this.vx += sin( this.theta ) * 0.1;
                this.vy += cos( this.theta ) * 0.1;

                this.radius *= 0.96;
                this.alive = this.radius > 0.5;
            },

            draw: function( ctx ) {

                ctx.beginPath();
                ctx.arc( this.x, this.y, this.radius, 0, TWO_PI );
                ctx.fillStyle = this.color;
                ctx.fill();
            }
        };

        // ----------------------------------------
        // Example
        // ----------------------------------------

        var MAX_PARTICLES = 280;
        var COLOURS = [ '#69D2E7', '#A7DBD8', '#E0E4CC', '#F38630', '#FA6900', '#FF4E50', '#F9D423' ];

        var particles = [];
        var pool = [];

        var demo = Sketch.create({
            container: document.getElementById('container')
        });

        var coordinateArray = <%=JavaScript.Serialize(_Default.array) %>;

        demo.setup = function() {

            // Set off some initial particles.
            var i, x, y;

            for ( i = 0; i < 20; i++ ) {
                x = ( demo.width * 0.5 ) + random( -100, 100 );
                y = ( demo.height * 0.5 ) + random( -100, 100 );
                demo.spawn( x, y );
            }
        };

        demo.spawn = function( x, y ) {

            if ( particles.length >= MAX_PARTICLES )
                pool.push( particles.shift() );

            particle = pool.length ? pool.pop() : new Particle();
            particle.init( x, y, random( 5, 40 ) );

            particle.wander = random( 0.5, 2.0 );
            particle.color = random( COLOURS );
            particle.drag = random( 0.9, 0.99 );

            theta = random( TWO_PI );
            force = random( 2, 8 );

            particle.vx = sin( theta ) * force;
            particle.vy = cos( theta ) * force;

            particles.push( particle );
        };

        demo.update = function() {

            var i, particle;

            for ( i = particles.length - 1; i >= 0; i-- ) {

                particle = particles[i];

                if ( particle.alive ) particle.move();
                else pool.push( particles.splice( i, 1 )[0] );
            }
        };

        demo.draw = function() {

            demo.globalCompositeOperation  = 'lighter';

            for ( var i = particles.length - 1; i >= 0; i-- ) {
                particles[i].draw( demo );
            }
        };

        function animateCoordinates() {
            requestAnimationFrame(animateCoordinates);

            if (coordinateArray == null || coordinateArray.length == 0) {
                return;
            }

            demo.spawn(coordinateArray.shift(), coordinateArray.shift());
        }

        animateCoordinates();

        //var str = "";
        //demo.mousemove = function () {
        //    str += demo.touches[0].x + "," + demo.touches[0].y + ",";
        //    console.clear();
        //    console.log(str);
        //}

        <% if(_Default.freeModeEnabled) {%>
            demo.mousemove = function() {

                var particle, theta, force, touch, max, i, j, n;

                for ( i = 0, n = demo.touches.length; i < n; i++ ) {

                    touch = demo.touches[i], max = random( 1, 4 );
                    for ( j = 0; j < max; j++ ) {
                        demo.spawn( touch.x, touch.y );
                    }

                }
            };
        <% } %>
    </script>
    </form>
</body>
</html>
