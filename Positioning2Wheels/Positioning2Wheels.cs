using EventArgsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Positioning2WheelsNS
{
    public class Positioning2Wheels
    {
        int robotId;
        Location robotLocation = new Location(0, 0, 0, 0, 0, 0);
        double fEch = 50;

        public Positioning2Wheels(int id)
        {
            robotId = id;
        }

        public void OnOdometryRobotSpeedReceived(object sender, PolarSpeedArgs e)
        {
            //A calculer...
            robotLocation.X += e.Vx / fEch * Math.Cos(robotLocation.Theta);
            robotLocation.Y += e.Vy / fEch * Math.Sin(robotLocation.Theta);
            robotLocation.Theta += e.Vtheta / fEch;
            OnCalculatedLocation(robotId, robotLocation);
        }

        //Output events
        public event EventHandler<LocationArgs> OnCalculatedLocationEvent;
        public virtual void OnCalculatedLocation(int id, Location locationRefTerrain)
        {
            var handler = OnCalculatedLocationEvent;
            if (handler != null)
            {
                handler(this, new LocationArgs { RobotId = id, Location = locationRefTerrain });
            }
        }
    }
}
