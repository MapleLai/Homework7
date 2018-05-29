# 用粒子系统做一个小行星带
--------------
## 介绍
最近课堂教了unity3D里面粒子系统的一些相关知识，结合之前做过的太阳系作业，所以这一次打算用粒子系统做一个小行星带。

## 情景
+ [演示视频](https://pan.baidu.com/s/16X_Lhv7xgppAtL1lWT3LeQ)
+ 游戏场景
![场景](https://raw.githubusercontent.com/MapleLai/Homework7/master/Screenshot/%E5%9C%BA%E6%99%AF.png)
![运行1](https://raw.githubusercontent.com/MapleLai/Homework7/master/Screenshot/%E8%BF%90%E8%A1%8C1.jpg)
![运行2](https://raw.githubusercontent.com/MapleLai/Homework7/master/Screenshot/%E8%BF%90%E8%A1%8C2.png)

## 制作情况
+ 首先，要先建立一个简单的太阳系，为了能够清楚观察，这个简单太阳系只包含太阳、火星和火星的小行星带（我知道火星是没有小行星带的，但做完了才想起来这茬，就将就一下吧！）。
+ 给火星添加一个空的子游戏对象，在这个空对象上添加粒子系统，最后整个游戏的游戏对象结构如图所示：
![游戏对象](https://raw.githubusercontent.com/MapleLai/Homework7/master/Screenshot/%E6%B8%B8%E6%88%8F%E5%AF%B9%E8%B1%A1.png)
+ Rotation类  
太阳和火星的自转代码：
<pre>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotation : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround(this.transform.position, Vector3.up, -1);
	}
}
</pre>

+ Revolution类  
实现火星公转的代码：
<pre>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Revolution : MonoBehaviour {
	Vector3 Sun= new Vector3(0, 0, 0);

	void Update () {
		Vector3 axis = new Vector3(0, 1, 1);
		this.transform.RotateAround(Sun, axis, 50 * Time.deltaTime);
	}
}
</pre> 

+ Particle类  
接下来就是重头戏，有关粒子系统的代码。Particle类是粒子的基本属性类，负责记录单个粒子的位置信息。
<pre>
public class Particle
{
	//粒子与圆环圆心的距离以及水平倾斜角
	public float radius;
	public float angle;

	public Particle()  
	{  
		this.radius = 0f;    
		this.angle = 0f;        
	}

	public Particle(float _radius, float _angle)  
	{  
		radius = _radius;    
		angle = _angle;      
	}
}
</pre>

+ RunParticle类  
初始化每个粒子的位置，还有实现粒子群的旋转运动。
<pre>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticle : MonoBehaviour {

	private ParticleSystem pSys; //游戏对象上的粒子系统
	private ParticleSystem.Particle[] particles; //存放粒子
	private Particle[] particleProperty; //存放粒子的位置信息

	// Use this for initialization
	void Start () 
	{
    //获取游戏对象的粒子系统
		particles = new ParticleSystem.Particle[5000];  
		particleProperty = new Particle[5000];  
		pSys = this.GetComponent<ParticleSystem>();  
    
    //初始化粒子数目和大小
		pSys.maxParticles = 5000;
    pSys.startSize = 0.05f;

    //因为粒子的运动由代码实现，所以需要让关于粒子运动的属性无效
		pSys.startSpeed = 0;            
		pSys.loop = false;    
    
    //发射粒子
		pSys.Emit(5000);  
		pSys.GetParticles(particles);

    //生成小行星带
		for (int i = 0; i < 5000; i++) 
		{
      //随机生成粒子的半径和水平倾斜角
			float minRadius = 2.0f * Random.Range(1.0f, 3.0f / 2.0f);  
			float maxRadius = 4.0f * Random.Range(3.0f / 4.0f, 1.0f);  
			float radius = Random.Range(minRadius, maxRadius);
			float angle = Random.Range(0.0f, 360.0f);

      //设置粒子的位置，让圆环沿Y轴方向微微倾斜，否则看到的是一条线
			particleProperty [i] = new Particle (radius, angle, time);
			particles [i].position = new Vector3 (particleProperty [i].radius * Mathf.Cos (particleProperty [i].angle / 180 * Mathf.PI), 															particleProperty [i].radius * Mathf.Sin (45 / 180 * Mathf.PI),										particleProperty [i].radius * Mathf.Sin (particleProperty [i].angle / 180 * Mathf.PI));
		}
		pSys.SetParticles(particles, 5000); 

	}
	
	//使各粒子按照一定角速度旋转
	void Update () 
	{
		for (int i = 0; i < 5000; i++)  
		{   
			particleProperty[i].angle -= 10f; 
			particleProperty[i].angle = particleProperty[i].angle % 360.0f;  

			particles [i].position = new Vector3 (particleProperty [i].radius * Mathf.Cos (particleProperty [i].angle / 180 * Mathf.PI), 															particleProperty [i].radius * Mathf.Sin (45 / 180 * Mathf.PI),															particleProperty [i].radius * Mathf.Sin (particleProperty [i].angle / 180 * Mathf.PI)); 
		}  
		pSys.SetParticles(particles, 5000);
	}
}

</pre>
