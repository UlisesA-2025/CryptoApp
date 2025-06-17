// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import TransactionsView from '../views/TransactionsView.vue'
import NewTransactionView from '../views/NewTransactionView.vue'
import TransactionDetailView from '../views/TransactionDetailView.vue'
import PortfolioView from '../views/PortfolioView.vue'

const routes = [
  { path: '/', name: 'Home', component: HomeView },
  { path: '/transactions', name: 'Transactions', component: TransactionsView },
  { path: '/transactions/new', name: 'NewTransaction', component: NewTransactionView },
  { path: '/transactions/:id', name: 'TransactionDetail', component: TransactionDetailView },
  { path: '/portfolio', name: 'Portfolio', component: PortfolioView }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
