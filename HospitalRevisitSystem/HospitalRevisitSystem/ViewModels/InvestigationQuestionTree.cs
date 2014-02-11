using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace HospitalRevisitSystem.ViewModels
{
    public class InvestigationQuestionTree
    {
        public static ArrayList getAllQuestions(  dbHospitalRevisitEntities dbContext)
        {
            ArrayList treeList = new ArrayList();
            foreach (tbInvestigation_Question q in dbContext.tbInvestigation_Question)
            {
                
                InvestigationQuestionTreeModel tree = new InvestigationQuestionTreeModel();
                tree.Id = q.Investigation_Question_ID;
                tree.Name = q.Review_Content;

                var query = from s in dbContext.tbInvestigation_Key
                            where s.Iinvestigatio_Question_ID == q.Investigation_Question_ID
                            select s;
                foreach(tbInvestigation_Key k in query.ToList())
                {
                    TreeNode node = new TreeNode();
                    node.Id = k.Investigation_Key_ID;
                    node.Name = k.Answer_Content;
                    tree.List.Add(node);
                }
                treeList.Add(tree);
            }
            return treeList;
        }

    }
    public class InvestigationQuestionTreeModel
    {
        public InvestigationQuestionTreeModel()
        {
            this.List = new List<TreeNode>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IList<TreeNode> List { get; private set; }

        public bool IsChild
        {
            get
            {
                return this.List.Count == 0;
            }
        }

    }
    public class TreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}