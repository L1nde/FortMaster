using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Research;
using Assets.Scripts.Turrets;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class ResearchTreeNode {


    public string researchName;
    public float xpCost;
    public bool researched;
    private HashSet<ResearchTreeNode> childs = new HashSet<ResearchTreeNode>();
    public HashSet<ResearchTreeNode> prerequisites = new HashSet<ResearchTreeNode>();

    [NonSerialized] public ResearchButton button;
    [NonSerialized] private ResearchItem item;


    public ResearchTreeNode(ResearchItem item) {
        this.researchName = item.researchName;
        this.xpCost = item.xpCost;
        this.researched = item.researched;
        this.item = item;

        
    }

    public ResearchTreeNode createTree() {
        foreach (var child in item.getChilds()) {
            var node = new ResearchTreeNode(child);
            if (!childs.Contains(node)) {
                this.childs.Add(node);
                node.addParent(this);
                node.createTree();
            }
        }

        return this;
    }

    public void addParent(ResearchTreeNode node) {
        prerequisites.Add(node);
    }

    public ResearchItem Item {
        get { return item; }
    }

    public string ResearchName {
        get { return researchName; }
    }

    public float XpCost {
        get { return xpCost; }
    }

    public bool Researched {
        get { return researched; }
    }

    public HashSet<ResearchTreeNode> Childs {
        get { return childs; }
    }

    public HashSet<StructureBlockData> getResearchedSB() {
        return getResearchedSB(new HashSet<StructureBlockData>());
    }

    public HashSet<StructureBlockData> getResearchedSB(HashSet<StructureBlockData> blocks) {
        if (researched) {
            if (item is ResearchBlock)  {
                blocks.Add(((ResearchBlock) item).block);
            }

            foreach (var child in childs) {
                blocks.UnionWith(child.getResearchedSB(blocks));
            }
        }

        return blocks;
    }

    public HashSet<TurretData> getResearchedTurrets() {
        return getResearchedTurrets(new HashSet<TurretData>());
    }

    public HashSet<TurretData> getResearchedTurrets(HashSet<TurretData> blocks) {
        if (researched) {
            if (item is ResearchTurret) {
                blocks.Add(((ResearchTurret)item).block);
            }

            foreach (var child in childs) {
                blocks.UnionWith(child.getResearchedTurrets(blocks));
            }
        }
        return blocks;
    }

    public List<string> getResearchedItemsNames() {
        return getResearchedItemsNames(new HashSet<string>()).ToList();
    }

    private HashSet<string> getResearchedItemsNames(HashSet<string> names) {
        if (researched) {
            names.Add(researchName);
        }

        foreach (var child in childs) {
            names.UnionWith(child.getResearchedItemsNames(names));
        }

        return names;
    }

    public ResearchTreeNode FindNode(string name) {
        if (researchName == name) {
            
            return this;
        }
        
        foreach (var child in childs) {
            var node = child.FindNode(name);
            if (node != null) {
                return node;
            }
        }

        return null;
    }
}