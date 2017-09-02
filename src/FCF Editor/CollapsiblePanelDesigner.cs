using System;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace FCF_Editor
{
    public class CollapsiblePanelDesigner : ParentControlDesigner
    {

        public override System.ComponentModel.Design.DesignerActionListCollection ActionLists
        {
            get
            {
                DesignerActionListCollection collection = new DesignerActionListCollection();
                if (this.Control != null && this.Control is CollapsiblePanel)
                {
                    CollapsiblePanel panel = (CollapsiblePanel)this.Control;
                    if (!String.IsNullOrEmpty(panel.Name))
                    {
                        if (String.IsNullOrEmpty(panel.HeaderText))
                            panel.HeaderText = panel.Name;
                    }
                }

                collection.Add(new CollapsiblePanelActionList(this.Control));
                
                return collection;
            }
        }

       


        
    }
}
