(this["webpackJsonpgithub-projects"]=this["webpackJsonpgithub-projects"]||[]).push([[0],{133:function(e,t,n){},134:function(e,t,n){},145:function(e,t,n){"use strict";n.r(t);var r=n(0),a=n.n(r),i=n(26),c=n.n(i),s=(n(133),n(193)),o=n(194),d=(n(134),n(101)),l=n(202),u=n(192),h=n(207),j=n(206),f=n(208),p=n(205),x=n(25),b=n.n(x),O=n(44),m=n(39),v=n(40),g=n(71),C=n(70),y=n(60),w=n(203),k=n(209),S=n(196),L=n(199),A=n(37),E=n(51),T=n(216),D=n(218),I=n(200),R=n(221),H=n(222),W=n(210),N=n(220),U=n(97),z=n.n(U),P=n(219),J=n(14),F=n(195),V=n(211),q=n(217),M=n(214),B=n(215),G=n(213),K=n(96),Q=n.n(K),X=n(1);function Y(e){var t=e.title,n=e.mode,a=e.description,i=e.deadline,c=e.onSaveCard,s="edit"===n,o=r.useState(t),d=Object(J.a)(o,2),l=d[0],u=d[1],h=r.useState(a),j=Object(J.a)(h,2),f=j[0],p=j[1],x=r.useState(new Date(i).toLocaleDateString("en-CA")),b=Object(J.a)(x,2),O=b[0],m=b[1],v=r.useState(!1),g=Object(J.a)(v,2),C=g[0],y=g[1],w=r.useState(!1),k=Object(J.a)(w,2),A=k[0],E=k[1],T=function(){y(!0)},D=function(){y(!1)},I=function(){E(!1)};return Object(X.jsxs)("div",{children:[s?Object(X.jsx)(P.a,{"aria-label":"edit",color:"primary",size:"small",onClick:T,children:Object(X.jsx)(Q.a,{fontSize:"small"})}):Object(X.jsx)(W.a,{variant:"contained",onClick:T,sx:{width:1},color:"primary",children:"New Card"}),Object(X.jsxs)(V.a,{open:C,onClose:D,children:[Object(X.jsx)(G.a,{children:s?"Edit card":"Add new card"}),Object(X.jsxs)(M.a,{children:[Object(X.jsx)(B.a,{children:s?"Edit the card.":"Fill out the fields and add to current lane."}),Object(X.jsx)(F.a,{autoFocus:!0,required:!0,margin:"dense",id:"name",label:"Title",type:"title",fullWidth:!0,variant:"outlined",onChange:function(e){u(e.target.value)},defaultValue:l}),Object(X.jsx)(F.a,{margin:"dense",id:"description",label:"Description",type:"text",fullWidth:!0,multiline:!0,rows:3,variant:"outlined",onChange:function(e){p(e.target.value)},defaultValue:f}),Object(X.jsx)(F.a,{id:"date",label:"",type:"date",onChange:function(e){m(e.target.value)},defaultValue:O})]}),Object(X.jsxs)(q.a,{children:[Object(X.jsx)(W.a,{onClick:D,variant:"outlined",children:"Cancel"}),Object(X.jsx)(W.a,{onClick:function(){l&&O?(c({title:l,description:f||"",deadline:O}),y(!1)):E(!0)},variant:"outlined",children:s?"Edit":"Add"})]}),Object(X.jsx)(L.a,{open:A,autoHideDuration:6e3,onClose:I,anchorOrigin:{horizontal:"center",vertical:"top"},children:Object(X.jsx)(S.a,{onClose:I,severity:"error",sx:{width:"100%"},children:"Title and deadline are required fields."})})]})]})}function Z(e){var t=e.id,n=e.title,r=e.description,a=e.deadline,i=e.laneId,c=e.onRemoveCard,s=e.onEditCard;return Object(X.jsx)(D.a,{sx:{width:1,padding:1,maxWidth:1},children:Object(X.jsx)(N.a,{primary:Object(X.jsxs)(w.a,{container:!0,spacing:2,sx:{width:1},style:{whiteSpace:"pre-wrap",overflowWrap:"break-word"},children:[Object(X.jsx)(w.a,{item:!0,xs:8,children:n}),Object(X.jsx)(w.a,{item:!0,xs:2,children:Object(X.jsx)(Y,{title:n,description:r,deadline:a,onSaveCard:function(e){s({title:e.title,description:e.description,deadline:e.deadline,id:t,laneId:i})},mode:"edit"})}),Object(X.jsx)(w.a,{item:!0,xs:2,children:Object(X.jsx)(P.a,{"aria-label":"delete",color:"primary",size:"small",onClick:function(){c(t)},children:Object(X.jsx)(z.a,{fontSize:"small"})})})]}),secondary:Object(X.jsxs)(X.Fragment,{children:[Object(X.jsx)(p.a,{sx:{display:"inline"},variant:"body2",color:"text.primary",component:"span",style:{wordWrap:"break-word"},children:new Date(a).toLocaleDateString({weekday:"long",year:"numeric",month:"long",day:"numeric"})}),Object(X.jsx)(p.a,{sx:{display:"inline",maxWidth:1},variant:"body2",component:"span",color:"text.secondary",style:{wordWrap:"break-word"},children:""===r.trim()||null===r?"":" - ".concat(r)})]})})})}Y.defaultProps={title:"",description:"",deadline:(new Date).toLocaleDateString("en-CA"),mode:"add"};var $=function(e){Object(g.a)(n,e);var t=Object(C.a)(n);function n(e){var r;return Object(m.a)(this,n),r=t.call(this,e),Object.assign(Object(E.a)(r),e),r}return Object(v.a)(n,[{key:"renderCard",value:function(e){var t=this;return Object(X.jsx)(Z,{id:this.cards[e].id,title:this.cards[e].title,description:this.cards[e].description,laneId:this.cards[e].laneID,deadline:this.cards[e].deadline,onRemoveCard:function(e){t.onRemoveCard(t.id,e)},onEditCard:function(e){t.onEditCard(t.id,e)},oreder:e})}},{key:"render",value:function(){var e=this;this.cards.sort((function(e,t){return e.order<t.order?-1:e.order>t.order?1:0}));var t=this.cards.map((function(t,n){return Object(X.jsx)(y.b,{draggableId:t.id.toString(),index:n,children:function(t){return Object(X.jsx)(I.a,Object(A.a)(Object(A.a)(Object(A.a)({alignitems:"flex-start"},t.draggableProps),t.dragHandleProps),{},{ref:t.innerRef,children:e.renderCard(n)}))}},t.id)}));return Object(X.jsxs)(D.a,{sx:{bgcolor:"#9e9e9e",marginTop:1,marginLeft:1,maxWidth:1},children:[Object(X.jsxs)(w.a,{container:!0,direction:"row",spacing:.5,children:[Object(X.jsx)(w.a,{item:!0,xs:8,children:Object(X.jsx)(Y,{onSaveCard:function(t){e.onAddNewCard(e.id,t)},mode:"new"})}),Object(X.jsx)(w.a,{item:!0,xs:4,children:Object(X.jsx)(W.a,{variant:"contained",sx:{width:1},color:"error",onClick:function(){e.onRemoveLane(e.id)},children:"Delete"})})]}),Object(X.jsx)(R.a,{}),Object(X.jsx)(H.a,{title:this.title,style:{textAlign:"center"}}),Object(X.jsx)(R.a,{}),Object(X.jsx)(y.c,{droppableId:this.id.toString(),children:function(e){return Object(X.jsxs)(T.a,Object(A.a)(Object(A.a)({alignitems:"stretch"},e.droppableProps),{},{ref:e.innerRef,children:[t,e.placeholder]}))}})]})}}]),n}(a.a.Component),_=function(){function e(t){Object(m.a)(this,e),this.host="https://localhost:5001",this.URL="https://localhost:5001/api",this.tryLimit=5,this.timeBetweenTriesMs=500,this.errorHandler=t}return Object(v.a)(e,[{key:"getAll",value:function(){var e=Object(O.a)(b.a.mark((function e(){var t,n,r=this;return b.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:n=0;case 1:if(!(void 0===t&&n<this.tryLimit)){e.next=11;break}return e.next=4,fetch("".concat(this.URL,"/Lanes"),{method:"GET",mode:"cors",withCredentials:!0,headers:{"Access-Control-Allow-Origin":this.host}}).catch((function(){r.errorHandler()}));case 4:if(t=e.sent,n+=1,void 0!==t){e.next=9;break}return e.next=9,setTimeout(null,this.timeBetweenTriesMs);case 9:e.next=1;break;case 11:return e.next=13,typeof t;case 13:if(e.t0=e.sent,"undefined"!==e.t0){e.next=18;break}e.t1=t,e.next=19;break;case 18:e.t1=t.json();case 19:return e.abrupt("return",e.t1);case 20:case"end":return e.stop()}}),e,this)})));return function(){return e.apply(this,arguments)}}()},{key:"postCard",value:function(){var e=Object(O.a)(b.a.mark((function e(t){var n,r=this;return b.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,fetch("".concat(this.URL,"/Cards"),{method:"POST",mode:"cors",headers:{"Access-Control-Allow-Origin":this.host,"Content-Type":"application/json"},body:JSON.stringify(t)}).catch((function(){r.errorHandler()}));case 2:return n=e.sent,e.next=5,typeof n;case 5:if(e.t0=e.sent,"undefined"!==e.t0){e.next=10;break}e.t1=n,e.next=11;break;case 10:e.t1=n.json();case 11:return e.abrupt("return",e.t1);case 12:case"end":return e.stop()}}),e,this)})));return function(t){return e.apply(this,arguments)}}()},{key:"editCard",value:function(e){var t=this;fetch("".concat(this.URL,"/Cards/").concat(e.id,"/edit"),{method:"PUT",mode:"cors",headers:{"Access-Control-Allow-Origin":this.host,"Content-Type":"application/json"},body:JSON.stringify(e)}).catch((function(){t.errorHandler()}))}},{key:"moveCard",value:function(e){var t=this;fetch("".concat(this.URL,"/Cards/").concat(e.id,"/move"),{method:"PUT",mode:"cors",headers:{"Access-Control-Allow-Origin":this.host,"Content-Type":"application/json"},body:JSON.stringify(e)}).catch((function(){t.errorHandler()}))}},{key:"deleteCard",value:function(e){var t=this;fetch("".concat(this.URL,"/Cards/").concat(e),{method:"DELETE",mode:"cors",headers:{"Access-Control-Allow-Origin":this.host}}).catch((function(){t.errorHandler()}))}},{key:"postLane",value:function(){var e=Object(O.a)(b.a.mark((function e(t){var n,r=this;return b.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,fetch("".concat(this.URL,"/Lanes"),{method:"POST",mode:"cors",headers:{"Access-Control-Allow-Origin":this.host,"Content-Type":"application/json"},body:JSON.stringify(t)}).catch((function(){r.errorHandler()}));case 2:return n=e.sent,e.next=5,typeof n;case 5:if(e.t0=e.sent,"undefined"!==e.t0){e.next=10;break}e.t1=n,e.next=11;break;case 10:e.t1=n.json();case 11:return e.abrupt("return",e.t1);case 12:case"end":return e.stop()}}),e,this)})));return function(t){return e.apply(this,arguments)}}()},{key:"deleteLane",value:function(e){var t=this;fetch("".concat(this.URL,"/Lanes/").concat(e),{method:"DELETE",mode:"cors",headers:{"Access-Control-Allow-Origin":this.host}}).catch((function(){t.errorHandler()}))}}]),e}(),ee=n(99),te=n.n(ee);function ne(e){var t=e.onNewLane,n=r.useState(""),a=Object(J.a)(n,2),i=a[0],c=a[1],s=r.useState(1),o=Object(J.a)(s,2),d=o[0],l=o[1],u=r.useState(!1),h=Object(J.a)(u,2),j=h[0],f=h[1],p=r.useState(!1),x=Object(J.a)(p,2),b=x[0],O=x[1],m=function(){f(!1)},v=function(){O(!1)};return Object(X.jsxs)(w.a,{container:!0,spacing:0,item:!0,alignItems:"stretch",direction:"row",xs:12,sm:6,md:4,lg:2,sx:{maxWidth:1,marginTop:1},children:[Object(X.jsx)(W.a,{variant:"outlined",sx:{height:1,width:1,minHeight:"50px",maxWidth:1},onClick:function(){f(!0)},children:Object(X.jsx)(te.a,{fontSize:"large"})}),Object(X.jsxs)(V.a,{open:j,onClose:m,children:[Object(X.jsx)(G.a,{children:"Add new lane"}),Object(X.jsxs)(M.a,{children:[Object(X.jsx)(B.a,{children:"Fill out the fields and add to the board."}),Object(X.jsx)(F.a,{autoFocus:!0,required:!0,margin:"dense",id:"name",label:"Title",type:"title",fullWidth:!0,variant:"outlined",onChange:function(e){c(e.target.value)},defaultValue:i}),Object(X.jsx)(F.a,{margin:"dense",id:"order",label:"Order",type:"number",fullWidth:!0,variant:"outlined",onChange:function(e){l(e.target.value)},defaultValue:d})]}),Object(X.jsxs)(q.a,{children:[Object(X.jsx)(W.a,{onClick:m,variant:"outlined",children:"Cancel"}),Object(X.jsx)(W.a,{onClick:function(){i&&d?(t({title:i,order:d-1}),f(!1)):O(!0)},variant:"outlined",children:"Add"})]}),Object(X.jsx)(L.a,{open:b,autoHideDuration:6e3,onClose:v,anchorOrigin:{horizontal:"center",vertical:"top"},children:Object(X.jsx)(S.a,{onClose:v,severity:"error",sx:{width:"100%"},children:"Title and order are required fields."})})]})]})}var re=function(e){Object(g.a)(n,e);var t=Object(C.a)(n);function n(e){var r;Object(m.a)(this,n),(r=t.call(this,e)).compareLaneOrders=function(e,t){return e.order<t.order?-1:e.order>t.order?1:0},r.state={lanes:[],isLoading:!0,isError:!1};return r.httpCommunication=new _((function(){r.setState({isError:!0}),r.setState({isLoading:!0})})),r}return Object(v.a)(n,[{key:"componentDidMount",value:function(){var e=Object(O.a)(b.a.mark((function e(){var t;return b.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,this.httpCommunication.getAll();case 2:return t=e.sent,e.next=5,t;case 5:if(!e.sent){e.next=14;break}return e.next=8,t.sort(this.compareLaneOrders);case 8:return e.next=10,this.setState({lanes:t});case 10:return e.next=12,this.setState({isLoading:!1});case 12:return e.next=14,this.setState({isError:!1});case 14:case"end":return e.stop()}}),e,this)})));return function(){return e.apply(this,arguments)}}()},{key:"renderLane",value:function(e){var t=this,n=function(){var e=Object(O.a)(b.a.mark((function e(n,r){var a,i,c,s;return b.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return a=r,i=t.state.lanes,c=i.find((function(e){return e.id===n})),s=i.indexOf(c),i.splice(s,1),a.laneID=n,e.next=8,t.httpCommunication.postCard(r).then((function(e){void 0!==e&&(a.id=e.id)}));case 8:return e.next=10,c.cards.push(a);case 10:return e.next=12,i.splice(s,0,c);case 12:return e.next=14,t.setState({lanes:i});case 14:case"end":return e.stop()}}),e)})));return function(t,n){return e.apply(this,arguments)}}(),r=this.state.lanes[e],a=r.id,i=r.cards,c=r.title;return Object(X.jsx)(w.a,{container:!0,direction:"column",alignItems:"stretch",item:!0,xs:12,sm:6,md:4,lg:2,sx:{maxWidth:1},children:Object(X.jsx)($,{id:a,cards:i,title:c,onAddNewCard:n,onRemoveCard:function(e,n){var r=t.state.lanes,a=r.find((function(t){return t.id===e})),i=r.indexOf(a);r.splice(i,1),a.cards.splice(a.cards.indexOf(a.cards.find((function(e){return e.id===n}))),1),r.splice(i,0,a),t.setState({lanes:r}),t.httpCommunication.deleteCard(n)},onEditCard:function(e,n){var r=t.state.lanes,a=r.find((function(t){return t.id===e})),i=r.indexOf(a);r.splice(i,1);var c=a.cards.indexOf(a.cards.find((function(e){return e.id===n.id})));a.cards.splice(c,1),a.cards.splice(c,0,n),r.splice(i,0,a),t.setState({lanes:r}),t.httpCommunication.editCard(n)},onRemoveLane:function(e){var n=t.state.lanes,r=n.find((function(t){return t.id===e})),a=n.indexOf(r);n.forEach((function(e,t){e.order>n[a].order&&(n[t].order-=1)})),n.splice(a,1),t.setState({lanes:n}),t.httpCommunication.deleteLane(e)}})},a)}},{key:"render",value:function(){var e=this,t=this.state,n=t.isError,r=t.isLoading,a=t.lanes,i=function(){return Object(X.jsx)(L.a,{open:n,autoHideDuration:1e4,anchorOrigin:{horizontal:"center",vertical:"top"},children:Object(X.jsx)(S.a,{severity:"error",sx:{width:"100%"},children:"There was a problem. Please reload the page."})})};if(r)return Object(X.jsxs)("div",{children:[Object(X.jsx)(j.a,{sx:{display:"flex"},justifyContent:"center",alignItems:"center",style:{minHeight:"90vh"},children:Object(X.jsx)(k.a,{size:80})}),i()]});var c=a.map((function(t,n){return e.renderLane(n)})),s=function(){var t=Object(O.a)(b.a.mark((function t(n){var r,i;return b.a.wrap((function(t){for(;;)switch(t.prev=t.next){case 0:return i=a,(r=n).cards=[],i.forEach((function(e,t){e.order>=r.order&&(i[t].order+=1)})),t.next=6,e.httpCommunication.postLane(r).then((function(e){void 0!==e&&(r.id=e.id)}));case 6:return t.next=8,i.splice(r.order,0,r);case 8:return t.next=10,i.sort(e.compareLaneOrders);case 10:return t.next=12,e.setState({lanes:i});case 12:case"end":return t.stop()}}),t)})));return function(e){return t.apply(this,arguments)}}();return Object(X.jsxs)("div",{children:[Object(X.jsxs)(w.a,{container:!0,spacing:1,justifyContent:"left",alignItems:"stretch",sx:{margintop:1,height:1,width:1},children:[Object(X.jsx)(y.a,{onDragEnd:function(t){if(t.destination){var n=Array.from(a),r=n.find((function(e){return e.id===Number(t.source.droppableId)})).cards.splice(t.source.index,1);n.find((function(e){return e.id===Number(t.destination.droppableId)})).cards.splice(t.destination.index,0,r[0]),e.setState((function(){return{lanes:n}})),r[0].laneID=Number(t.destination.droppableId),r[0].order=t.destination.index,e.httpCommunication.moveCard(r[0])}},children:c}),Object(X.jsx)(ne,{onNewLane:s})]}),i()]})}}]),n}(a.a.Component),ae=re,ie=Object(d.a)({palette:{background:{default:"#e0e0e0"},primary:{main:"#424242"},secondary:{main:"#616161"}}});var ce=function(){return Object(X.jsxs)(l.a,{theme:ie,children:[Object(X.jsx)(u.a,{}),Object(X.jsx)(j.a,{sx:{flexGrow:1},children:Object(X.jsx)(h.a,{position:"static",children:Object(X.jsx)(f.a,{variant:"dense",children:Object(X.jsx)(p.a,{variant:"h6",color:"inherit",component:"div",children:"Tasks"})})})}),Object(X.jsx)(ae,{})]})};c.a.render(Object(X.jsx)(a.a.StrictMode,{children:Object(X.jsx)(s.a,{dateAdapter:o.a,children:Object(X.jsx)(ce,{})})}),document.getElementById("root"))}},[[145,1,2]]]);
//# sourceMappingURL=main.5feb16f1.chunk.js.map