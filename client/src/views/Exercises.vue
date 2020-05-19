<template>
  <div class="exercises">
    <div class="center content-inputs">
      <vs-input
        type="search"
        v-model="searchQuery"
        placeholder="Search by name"
      />
      <vs-button
        @click="getExercisesSearch()"
      >
        Search
      </vs-button>
      <br>
      <vs-select placeholder="Sort by" v-model="sortQuery">
        <vs-option label="Name" value="name">
          Name
        </vs-option>
      </vs-select>
      <br>
      <vs-select placeholder="Direction" v-model="directionQuery">
        <vs-option label="Ascending" value="asc">
          Ascending
        </vs-option>
        <vs-option selected label="Descending" value="desc">
          Descending
        </vs-option>
      </vs-select>
      <br>
      <vs-button
        class="delete"
        @click="getExercises()"
      >
        Clear filters
      </vs-button>
    </div>
    <div v-for="e in exercises" :key="e.id" class="exercise">
      <p id="name"><b>{{ e.name }}</b></p>
      <p id="description">{{ e.description }}</p>
      <a :href="e.videoUrl" target="_blank">Watch video</a>
      <vs-button @click="showBodyParts(e.targetParts)">Show affected body parts</vs-button>
      <vs-button color="warn" @click="showEditDialog = true; selectedExercise = e;">Edit</vs-button>
      <vs-button class="delete" @click="deleteExercise(e.id)">Delete</vs-button>
    </div>

    <vs-dialog v-model="showDialog" :loading="loading">
      <div v-for="bp in dialogBodyParts" :key="bp.id" class="body-part">
        <img style="max-width: 100px;" :src="bp.imageUrl" alt="">
        <p><b>{{ bp.name }}</b></p>
        <p>Location: {{ bp.location }}</p>
      </div>
    </vs-dialog>

    <vs-dialog v-model="showEditDialog">
      <div class="exercise-edit">
        <h4>
          <b>Edit exercise</b>
        </h4>

        <div>
          <vs-input class="margin-bottom" v-model="selectedExercise.name" placeholder="Name">
            <template #icon>
              <i class='bx bxs-rename'></i>
            </template>
          </vs-input>
          <vs-input class="margin-bottom" v-model="selectedExercise.description" placeholder="Description">
            <template #icon>
              <i class='bx bxs-credit-card-front' ></i>
            </template>
          </vs-input>
          <vs-input class="margin-bottom" type="url" v-model="selectedExercise.videoUrl" placeholder="Video url">
            <template #icon>
              <i class='bx bxs-videos' ></i>
            </template>
          </vs-input>
          <!-- <vs-select
            class="margin-bottom"
            multiple
            filter
            placeholder="Select body parts"
            v-model="selectedExercise.targetParts"
            v-if="bodyParts.length > 0"
          >
            <vs-option v-for="bp in bodyParts" :key="bp.id" :label="bp.name" :value="bp">
              {{ bp.name }}
            </vs-option>
          </vs-select> -->

          <vs-button style="margin: 0;" @click="editExercise()">
            Edit Exercise
          </vs-button>
        </div>
      </div>
    </vs-dialog>

    <div class="center">
      <vs-pagination v-model="page" :length="5" @change="console.log('test')"/>
    </div>
  </div>
</template>

<script>

export default {
  name: 'Exercises',
  data:() => ({
    exercises: [],
    bodyParts: [],
    selectedExercise: {},
    dialogBodyParts: [],
    showDialog: false,
    showEditDialog: false,
    loading: true,
    page: 1,
    searchQuery: "",
    sortQuery: "",
    directionQuery: ""
  }),
  watch: {
    page(val) {
      this.getExercises();
    },
    sortQuery(val) {
      this.getExercisesSort()
    },
    directionQuery(val) {
      this.getExercisesSort()
    }
  },
  methods: {
    openNotification(position = null, color, title, message) {
      const noti = this.$vs.notification({
        progress: 'auto',
        color,
        position,
        title: title,
        text: message
      })
    },
    async getBodyParts() {
      fetch(
        "https://cloud-api-276610.ew.r.appspot.com/api/v1/bodyparts",
        ).then((res) => {
          return res.json()
      }).then((bparts) => {
        this.bodyParts = bparts;
      })
    },
    getExercises() {
      const loading = this.$vs.loading({
        text: 'Loading...'
      });
      this.exercises = [];
      fetch(
        `https://cloud-api-276610.ew.r.appspot.com/api/v1/exercises?page=${this.page-1}&limit=2`,
        ).then((res) => {
          return res.json()
      }).then((data) => {
        if (data != null) {
          data.forEach(e => {
            this.exercises.push(e);
          });
        }
        loading.close();
      })
    },
    getExercisesSearch() {
      this.exercises = [];
      fetch(
        `https://cloud-api-276610.ew.r.appspot.com/api/v1/exercises?name=${this.searchQuery}`,
        ).then((res) => {
          return res.json()
      }).then((data) => {
        if (data != null) {
          data.forEach(e => {
            this.exercises.push(e);
          });
        }
      })
    },
    getExercisesSort() {
      this.exercises = [];
      fetch(
        `https://cloud-api-276610.ew.r.appspot.com/api/v1/exercises?sortBy=${this.sortQuery}&direction=${this.directionQuery}&page=${this.page}&limit=2`,
        ).then((res) => {
          return res.json()
      }).then((data) => {
        if (data != null) {
          data.forEach(e => {
            this.exercises.push(e);
          });
        }
      })
    },
    async showBodyParts(targetParts) {
      this.loading = true;
      this.dialogBodyParts = [];
      this.showDialog = true;
      for (const tp of targetParts) {
        let res = await fetch("https://cloud-api-276610.ew.r.appspot.com/api/v1/bodyparts/" + tp.bodyPartId);
        let bp = await res.json();
        this.dialogBodyParts.push(bp);
      };
      this.loading = false;
    },
    async editExercise() {
      // let targetPartsIds = [];
      // if (this.selectedExercise.targetParts != null) {
      //   this.selectedExercise.targetParts.forEach(tp => {
      //     if (tp != null && tp.id != null && tp.id != undefined) {
      //       targetPartsIds.push(
      //         {
      //           bodyPartId: tp.id
      //         }
      //       )
      //     }
      //   });
      // }

      // console.log(targetPartsIds)
      
      let res = await fetch(`https://cloud-api-276610.ew.r.appspot.com/api/v1/exercises/${this.selectedExercise.id}`,
      {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          name: this.selectedExercise.name,
          description: this.selectedExercise.description,
          videoUrl: this.selectedExercise.videoUrl,
          //targetParts: targetPartsIds
        }),
      });
      
      if (res.ok) {
        this.openNotification(
          'top-right',
          'success',
          'Exercise added',
          'The exercise was added to the database.');
      } else {
        this.openNotification(
          'top-right',
          'danger',
          'Failed to add exercise',
          'Name, description are required and video url should be valid.');
      }
    },
    async deleteExercise(id) {
      let res = await fetch("https://cloud-api-276610.ew.r.appspot.com/api/v1/exercises/" + id,
      {
        method: 'DELETE',
      });
      this.exercises = [];
      this.getExercises();
    }
  },
  mounted() {
    this.getExercises();
    this.getBodyParts();
  }
}
</script>

<style lang="scss" scoped>
.exercise {
  padding: 2rem;
  border-radius: 2rem;

  &:nth-child(odd) {
    background-color: #f5f5f5;
  }

  .vs-button {
    margin: 0;
    margin-top: 1rem;
  }
}
.delete {
  background-color: red;
}
.center {
  margin: 0 auto;
}
</style>