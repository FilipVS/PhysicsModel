using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ModelDisplay.Controls
{
    /// <summary>
    /// A radio button that can store extra information of some specifiend enum value
    /// </summary>
    class RadioButtonWithParticleType : RadioButton
    {

        /// <summary>
        /// The enum value
        /// </summary>
        public ParticleTypes ParticleType { get; set; } = ParticleTypes.StationaryPositive;

    }
}
