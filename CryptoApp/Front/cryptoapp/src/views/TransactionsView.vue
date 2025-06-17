<template>
  <div>
    <h1>Historial de Transacciones</h1>
    <router-link to="/transactions/new">Agregar Nueva Transacción</router-link>
    <table border="1" cellpadding="5" cellspacing="0">
      <thead>
        <tr>
          <th>Criptomoneda</th>
          <th>Acción</th>
          <th>Cantidad</th>
          <th>Monto (ARS)</th>
          <th>Fecha</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="tx in transactions" :key="tx.id">
          <td>{{ tx.cryptoCode }}</td>
          <td>{{ tx.action }}</td>
          <td>{{ tx.cryptoAmount }}</td>
          <td>{{ tx.money }}</td>
          <td>{{ formatDate(tx.dateTime) }}</td>
          <td>
            <button @click="viewTransaction(tx.id)">Ver</button>
            <button @click="editTransaction(tx.id)">Editar</button>
            <button @click="deleteTransaction(tx.id)">Eliminar</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import api from '../services/api'

const router = useRouter()
const transactions = ref([])

// 📌 Esta es la función que trae las transacciones
async function fetchTransactions() {
  try {
    const res = await api.get('/transactions')
    transactions.value = res.data
  } catch (error) {
    console.error('Error al cargar transacciones:', error)
    alert('Error al cargar transacciones. Ver consola.')
  }
}

// 📌 Esto carga las transacciones automáticamente cuando se abre la vista
onMounted(fetchTransactions)

onMounted(async () => {
  const res = await api.get('/transactions')
  transactions.value = res.data
})

function viewTransaction(id) {
  router.push(`/transactions/${id}`)
}

function editTransaction(id) {
  router.push(`/transactions/${id}?edit=true`)
}

async function deleteTransaction(id) {
  if (!confirm('¿Estás seguro de que querés eliminar esta transacción?')) {
    return;
  }

  try {
    await api.delete(`/transactions/${id}`)
    alert('Transacción eliminada correctamente.')
    fetchTransactions() // 🔄 Recarga la lista
  } catch (error) {
    console.error('Error al eliminar transacción:', error)
    alert('Error al eliminar transacción. Ver consola.')
  }
}

function formatDate(dateString) {
  if (!dateString) return 'Fecha no disponible'

  const date = new Date(dateString)

  if (isNaN(date)) return 'Fecha inválida'

  // 📌 Formato completo (fecha y hora)
  return date.toLocaleString('es-AR', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  })
}

</script>
