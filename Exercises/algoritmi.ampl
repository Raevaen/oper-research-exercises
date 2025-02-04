# Definizione del grafo (sostituire con i dati specifici)
set NODES := 1 2 3 4 5;
set EDGES := (1,2) (1,3) (2,4) (3,4) (4,5);

# -------------------------------
# 1. Insieme Stabile Massimale (Maximal Independent Set)
# -------------------------------
param active_NODES default NODES;
set MIS default {};

repeat {
    # Calcola i gradi dei nodi attivi
    param degree{n in active_NODES} := 
        card({(n,j) in EDGES: j in active_NODES} union {(j,n) in EDGES: j in active_NODES});
    
    # Trova il nodo con grado minimo
    param min_degree = min{n in active_NODES} degree[n];
    param selected_node symbolic;
    
    for{n in active_NODES: degree[n] == min_degree} {
        let selected_node := n;
        break;
    }
    
    # Aggiungi il nodo all'insieme stabile
    let MIS := MIS union {selected_node};
    
    # Rimuovi il nodo e i suoi vicini dal grafo attivo
    let active_NODES := active_NODES diff 
        ({selected_node} union {j in NODES: (selected_node,j) in EDGES or (j,selected_node) in EDGES});
    
} while card(active_NODES) > 0;

print "Maximal Independent Set:", MIS;

# -------------------------------
# 2. Abbinamento Massimale (Maximal Matching)
# -------------------------------
set M default {};
param covered_nodes within NODES default {};

for{(i,j) in EDGES} {
    if (i not in covered_nodes) and (j not in covered_nodes) {
        let M := M union {(i,j)};
        let covered_nodes := covered_nodes union {i,j};
    }
}

print "Maximal Matching:", M;

# -------------------------------
# 3. Copertura con Archi Minimale (Minimal Edge Cover)
# -------------------------------
set EC default {};
param covered default {};

for{v in NODES} {
    if v not in covered {
        # Scegli un arco incidente a v
        for{(v,j) in EDGES} {
            let EC := EC union {(v,j)};
            let covered := covered union {v,j};
            break;
        }
    }
}

print "Minimal Edge Cover:", EC;

# -------------------------------
# 4. Copertura con Nodi Minimale (Minimal Vertex Cover)
# -------------------------------
set VC default {};

for{(i,j) in EDGES} {
    if (i not in VC) and (j not in VC) {
        # Scegli il nodo con grado più alto (greedy)
        param deg_i := card({(i,k) in EDGES} union {(k,i) in EDGES});
        param deg_j := card({(j,k) in EDGES} union {(k,j) in EDGES});
        
        if deg_i >= deg_j {
            let VC := VC union {i};
        } else {
            let VC := VC union {j};
        }
    }
}

print "Minimal Vertex Cover:", VC;