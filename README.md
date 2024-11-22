# Travail-Session-Prog-Graph
Projet de session - Programmation Graphique

Travail collaboratif fait par:
Samuel Duguay
William Audy

# Plan de match

## Mise en contexte

*C'est plus la partie de la BD pour cette section...*

1) On va devoir faire un schéma UML
2) L'administrateur est capable de CRUD sur toutes les tables (ou presque)

## Utilisation du programme
***Les utilisateurs doivent pouvoir se déconnecter. (admin et autres)***

### Partie administrateur

1) Le programme débute sur la **page d'activités** du centre.
La page d'activité contient le nom et la moyenne des notes d'évaluation de chaque activité. (Gridview?)
2) **Authentification** dans une boîte de dialogue pour l'administrateur. (username et password) (validations et messages d'erreurs)
3) L'admin a des pages pour **CRUD** sur chaque table. (Adhérents, activités, séances)
4) Page de statistique qui affiche Nb total d'adhérents, nb total d'activités, nb d'ahérents pour chaque activités. moyenne notes d'appréciation de chaque activité. **On doit également faire 3 autres stats de notre choix qui inclus activités, séances et participants**.
5) **Exportation dans un fichier CSV** de la liste des adhérents et de la liste des activités.

### Partie publique

1) Un adhérent peut commencer sa session en entrant son numéro d'identification. Son nom apparait à l'écran. Il peut se déconnecter.
2) L'utilisateur ne voit pas les section d'admin.
3) Il peut cliquer sur une activité et choisir sa séance (combo-box?)
4) UNE SEULE INSCRIPTION par activité.
5) Il peut noter sur 5 une activité (donc la séance)
