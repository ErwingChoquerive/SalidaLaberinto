#include <iostream>
#include <bits/stdc++.h>

using namespace std;
char bosque[1002][1002];
int dist[1002][1002];
struct coordenada{
int x;
int y;
};
int n,m,x,y;
queue <coordenada> nodos;
int dx[4]={-1,0,+1,0};
int dy[4]={0,-1,0,+1};
int sx,sy,ex,ey;
void bfs(){
    memset(dist,-1,sizeof(dist));
    dist[ex][ey]=0;
    coordenada ce;
    ce.x=ex;
    ce.y=ey;
    nodos.push(ce);
//    int cont=0;
    while(!nodos.empty()){
        coordenada c_a;
        c_a=nodos.front();
        nodos.pop();
        //recorridonodos adyacentes (N,S,E,O)
        for(int i=0;i<4;i++){
            x=c_a.x+dx[i];
            y=c_a.y+dy[i];
            //cout <<x<<y<<endl;
            if(dist[x][y]==-1&&(bosque[x][y]=='S'||(bosque[x][y]>='0'&&bosque[x][y]<='9'))){               // distancia[nodos[n_a][i]]=distancia[n_a]+1;
//               cont++;
               // cout<<dist[c_a.x][c_a.y]+1<<" "<<x<<" "<<y<<endl;
                dist[x][y]=dist[c_a.x][c_a.y]+1;
                coordenada ca;
                ca.x=x;
                ca.y=y;
                //cola.push(nodos[n_a][i]);
                nodos.push(ca);
//                if(cont==10){
//                    return;
//                }
            }
        }

    }
}

int main0000()
{
   // cout<< (int)'0';
    cin >>n>>m;
    for(int i=1;i<=n;i++){
        cin>>bosque[i]+1;
        for(int j=1;j<=m;j++){
            if(bosque[i][j]=='E'){
                ex=i;
                ey=j;
            }
            if(bosque[i][j]=='S'){
                sx=i;
                sy=j;
            }
        }
    }
   // cout <<sx<<sy<<ex<<ey<<endl;
    bfs();
//    for(int i=1;i<=n;i++){
//        for(int j=1;j<=m;j++){
//            //cout<<dist[i][j]<<" ";
//            if(dist[i][j] != -1)
//            {
//                cout<<"_";
//            }
//            else if()
//                cout<<"T";
//        }
//        cout << endl;
//    }
//

   // cout<<dist[sx][sy]<<endl;
    int acum = 0;
    for(int i=1; i<=n; i++)
    {

        for(int j=1; j<=m; j++)
        {
            if(bosque[i][j] >= '0' && bosque[i][j]<='9' && dist[i][j] <= dist[sx][sy])
                acum += ((int)bosque[i][j]-48);
               // cout<< ((int)bosque[i][j]-48) <<" ";
        }
    }
    cout<<acum<<endl;
    return 0;
}
