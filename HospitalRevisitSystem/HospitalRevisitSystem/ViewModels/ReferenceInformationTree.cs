﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace HospitalRevisitSystem.ViewModels
{
    public class ReferenceInformationTree
    {
        public static void getAllChildren(int ID, ref TreeModel treeList, ref dbHospitalRevisitEntities dbContext)
        {
            tbReference_Information ri = dbContext.tbReference_Information.Find(ID);
            TreeModel tree = new TreeModel();
            tree.Id = ID;
            tree.Name = ri.Information_Content;
            tree.Sequence_Number = ri.Sequence_Number??0;
            var query = from s in dbContext.tbReference_Information
                        where s.Parent_ID == ID
                        select s;
            if (query.ToList().Count != 0)
            {

                foreach (tbReference_Information r in query.ToList())
                {
                    getAllChildren(r.Reference_Information_ID, ref tree,ref dbContext );
                }

            }
            treeList.List.Add(tree);
        }
    
    }
    public class TreeModel
    {
        public TreeModel()
        {
            this.List = new List<TreeModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence_Number;
        public IList<TreeModel> List { get; private set; }

        public bool IsChild
        {
            get
            {
                return this.List.Count == 0;
            }
        }

    }
}