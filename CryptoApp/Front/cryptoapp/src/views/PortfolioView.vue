<template>
  <div>
    <h1>Mi Portafolio</h1>
    <table border="1" cellpadding="5" cellspacing="0">
      <thead>
        <tr>
          <th>Criptomoneda</th>
          <th>Cantidad</th>
          <th>Valor Total (ARS)</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in portfolio" :key="item.cryptoCode">
          <td>{{ item.cryptoCode }}</td>
          <td>{{ item.amount }}</td>
          <td>{{ item.totalValue }}</td>
        </tr>
      </tbody>
    </table>
    <h3>Total en ARS: {{ total }}</h3>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import api from '../services/api'

const portfolio = ref([])
const total = ref(0)

onMounted(async () => {
  const res = await api.get('/portfolio') // Este endpoint lo tenés que crear en el backend
  portfolio.value = res.data.cryptos
  total.value = res.data.total
})
</script>
